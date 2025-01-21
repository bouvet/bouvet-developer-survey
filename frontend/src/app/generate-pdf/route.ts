import puppeteer from "puppeteer";

export async function GET(req: Request) {
  try {
    const browser = await puppeteer.launch({ headless: true });
    const page = await browser.newPage();

    // Get and validate query param year
    const requestUrl = new URL(req.url);
    const year = requestUrl.searchParams.get("year");

    if (!year || isNaN(Number(year)) || Number(year) <= 0) {
      // If the `year` is not provided or is not a valid number, return an error response
      const headers = new Headers();
      headers.set("Content-Type", "application/json");
      return new Response(
        JSON.stringify({
          message: "Invalid or missing 'year' query parameter",
        }),
        { headers, status: 400 } // HTTP 400 Bad Request
      );
    }

    const protocol = process.env.NODE_ENV === "development" ? "http" : "https";
    const url = `${protocol}://${req.headers.get("host")}/${year}`;

    await page.goto(url, {
      waitUntil: "networkidle0",
    });

    // To reflect CSS used for screens instead of print
    await page.emulateMediaType("screen");

    // Inject custom CSS to ensure correct page breaks for titles and blocks
    await page.addStyleTag({
      content: `
        /* Ensure that the survey title starts on a new page */
        .survey-section {
          page-break-before: always !important; /* Forces a page break before the title */
        }

        /* Ensure that the first block after a title stays on the same page */
        .survey-section + .survey-block {
          page-break-before: auto !important; /* No page break before the first block */
        }

        /* Ensure that every block after the first goes to a new page */
        .survey-block:nth-of-type(n+2) {
		  padding-top: 140px;
          page-break-before: always !important; /* Forces a page break before each subsequent block */
        }

        /* If blocks are within survey-title groups, ensure blocks don't split between pages */
        .survey-block {
          page-break-inside: avoid !important; /* Prevent block from splitting across pages */
        }
      `,
    });

    // Download the PDF
    const pdf = await page.pdf({
      format: "A4",
      printBackground: true,
      preferCSSPageSize: true,
      landscape: true,
    });

    await browser.close();

    const headers = new Headers();

    headers.set("Content-Type", "application/pdf");
    headers.set("Content-Disposition", "attachment; filename=faktura.pdf");

    return new Response(pdf, { headers });
  } catch (error: unknown) {
    //

    const headers = new Headers();
    headers.set("Content-Type", "application/json");
    return new Response(
      JSON.stringify({ message: "Something went wrong: ", error }),
      {}
    );
  }
}
