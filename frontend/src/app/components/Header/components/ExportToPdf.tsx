import { Document, Page, Text, View } from "@react-pdf/renderer";

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

export default ExportToPdf;
