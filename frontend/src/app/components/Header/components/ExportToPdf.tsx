import { Document, Page, pdf, Text, View } from "@react-pdf/renderer";
import saveAs from "save-file";

const handleDownload = async (year?: string) => {
  const blob = await pdf(<ExportToPdf />).toBlob();
  await saveAs(blob, `DeveloperSurvey${year}.pdf`);
};

const ExportToPdf = () => {
  return (
    <Document>
      <Page size="A4">
        <View>
          <Text>HEJ</Text>
        </View>
        <View>
          <Text>TJABBA</Text>
        </View>
      </Page>
    </Document>
  );
};

export default handleDownload;
