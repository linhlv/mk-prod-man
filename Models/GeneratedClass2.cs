using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kenrapid.CRM.Web.Models.Quotation;
using DocumentFormat.OpenXml.Packaging;
using Ap = DocumentFormat.OpenXml.ExtendedProperties;
using Vt = DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using X15ac = DocumentFormat.OpenXml.Office2013.ExcelAc;
using X14 = DocumentFormat.OpenXml.Office2010.Excel;
using X15 = DocumentFormat.OpenXml.Office2013.Excel;
using A = DocumentFormat.OpenXml.Drawing;
using Xdr = DocumentFormat.OpenXml.Drawing.Spreadsheet;

namespace Kenrapid.CRM.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class GeneratedClass2
    {
        /// <summary>
        /// 
        /// </summary>
        private QuotationModel _quotationModel;

        /// <summary>
        /// 
        /// </summary>
        private string _relativePath;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quotationModel"></param>
        /// <param name="relativePath"></param>
        public GeneratedClass2(QuotationModel quotationModel, string relativePath)
        {
            this._quotationModel = quotationModel;
            this._relativePath = relativePath;
        }

        // Creates a SpreadsheetDocument.
        public void CreatePackage(string filePath)
        {
            using (SpreadsheetDocument package = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {
                CreateParts(package);
            }
        }

        // Adds child parts and generates content of the specified part.
        private void CreateParts(SpreadsheetDocument document)
        {
            ExtendedFilePropertiesPart extendedFilePropertiesPart1 = document.AddNewPart<ExtendedFilePropertiesPart>("rId3");
            GenerateExtendedFilePropertiesPart1Content(extendedFilePropertiesPart1);

            WorkbookPart workbookPart1 = document.AddWorkbookPart();
            GenerateWorkbookPart1Content(workbookPart1);

            WorkbookStylesPart workbookStylesPart1 = workbookPart1.AddNewPart<WorkbookStylesPart>("rId3");
            GenerateWorkbookStylesPart1Content(workbookStylesPart1);

            ThemePart themePart1 = workbookPart1.AddNewPart<ThemePart>("rId2");
            GenerateThemePart1Content(themePart1);

            WorksheetPart worksheetPart1 = workbookPart1.AddNewPart<WorksheetPart>("rId1");
            GenerateWorksheetPart1Content(worksheetPart1);

            DrawingsPart drawingsPart1 = worksheetPart1.AddNewPart<DrawingsPart>("rId2");
            GenerateDrawingsPart1Content(drawingsPart1);

            ImagePart imagePart1 = drawingsPart1.AddNewPart<ImagePart>("image/png", "rId1");
            GenerateImagePart1Content(imagePart1);

            #region Draw Image
            RowsImageData(drawingsPart1);

            ImagePart imagePart3 = drawingsPart1.AddNewPart<ImagePart>("image/png", "rId3");
            GenerateImagePart3Content(imagePart3);
            #endregion

            SpreadsheetPrinterSettingsPart spreadsheetPrinterSettingsPart1 = worksheetPart1.AddNewPart<SpreadsheetPrinterSettingsPart>("rId1");
            GenerateSpreadsheetPrinterSettingsPart1Content(spreadsheetPrinterSettingsPart1);

            SharedStringTablePart sharedStringTablePart1 = workbookPart1.AddNewPart<SharedStringTablePart>("rId4");
            GenerateSharedStringTablePart1Content(sharedStringTablePart1);

            SetPackageProperties(document);

            Cell cell = GetCell(worksheetPart1.Worksheet, "B", 3);
            cell.CellValue = new CellValue(_quotationModel.Attn); //ATTN
            cell.DataType = new EnumValue<CellValues>(CellValues.String);
        }


        private void GenerateImageContent(ImagePart imagePart, string imageFile)
        {
            System.IO.Stream data = System.IO.File.OpenRead(this._relativePath + imageFile); //GetBinaryDataStream(imagePart2Data);
            imagePart.FeedData(data);
            data.Close();
        }

        #region Customs
        // Generates content of imagePart3.
        private void GenerateImagePart3Content(ImagePart imagePart3)
        {
            System.IO.Stream data = GetBinaryDataStream(imagePart3Data);
            imagePart3.FeedData(data);
            data.Close();
        }

        // Given a worksheet, a column name, and a row index, 
        // gets the cell at the specified column and 
        private Cell GetCell(Worksheet worksheet, string columnName, uint rowIndex)
        {
            Row row = GetRow(worksheet, rowIndex);

            if (row == null)
                return null;

            return row.Elements<Cell>().First(c => string.Compare
                (c.CellReference.Value, columnName +
                                        rowIndex, true) == 0);
        }

        // Given a worksheet and a row index, return the row.
        private static Row GetRow(Worksheet worksheet, uint rowIndex)
        {
            return worksheet.GetFirstChild<SheetData>().Elements<Row>().First(r => r.RowIndex == rowIndex);
        }

        #endregion

        #region Data
        private void RowsImageData(DrawingsPart drawingsPart)
        {
            #region Generate Images Data

            for (var i = 0; i < _quotationModel.QuotationItems.Count; i++)
            {
                ImagePart imagePart = drawingsPart.AddNewPart<ImagePart>("image/png", "rId" + Convert.ToString(i + 6));
                GenerateImageContent(imagePart, _quotationModel.QuotationItems[i].Picture);
            }

            #endregion
        }

        private void RowsData(SheetData sheetData1)
        {
            #region Row:Data

            //string [] colsArrays = new string[]{"A", "B", "C", "D", "E", "F", "G",  "H", "I", "J", "K"};
            //string [] colsArrays = new string[] { "A", /*"B",*/ "C", "D", "E", "F", "G", "H", "I", "J", "K" }; //Skip B
            int start = 7;
            for (var i = 0; i < this._quotationModel.QuotationItems.Count; i++)
            {
                var qi = this._quotationModel.QuotationItems[i];

                var row = new Row()
                {
                    RowIndex = System.Convert.ToUInt32(i + start),
                    Spans = new ListValue<StringValue>() { InnerText = "1:11" },
                    Height = 86.25D,
                    CustomHeight = true,
                    DyDescent = 0.25D
                };

                //Code No
                var cell = new Cell() { CellReference = "A" + (start + i), StyleIndex = (UInt32Value)15U };
                var cellValue = new CellValue
                {
                    Text = qi.CodeNo
                };
                cell.DataType = new EnumValue<CellValues>(CellValues.String);
                cell.Append(cellValue);
                row.Append(cell);

                //Picture
                cell = new Cell() { CellReference = "B" + (start + i), StyleIndex = (UInt32Value)12U };
                row.Append(cell);

                //Description
                cell = new Cell() { CellReference = "C" + (start + i), StyleIndex = (UInt32Value)13U };
                cellValue = new CellValue { Text = qi.Description };
                cell.DataType = new EnumValue<CellValues>(CellValues.String);
                cell.Append(cellValue);
                row.Append(cell);

                //Size
                cell = new Cell() { CellReference = "D" + (start + i), StyleIndex = (UInt32Value)12U };
                cellValue = new CellValue { Text = qi.Size };
                cell.DataType = new EnumValue<CellValues>(CellValues.String);
                cell.Append(cellValue);
                row.Append(cell);

                //Price FOB
                cell = new Cell() { CellReference = "E" + (start + i), StyleIndex = (UInt32Value)12U };
                cellValue = new CellValue { Text = "" + qi.PriceFOB };
                cell.DataType = new EnumValue<CellValues>(CellValues.Number);
                cell.Append(cellValue);
                row.Append(cell);

                //PCS/SE :: NOTE
                cell = new Cell() { CellReference = "F" + (start + i), StyleIndex = (UInt32Value)16U };
                cellValue = new CellValue { Text = "PCS/SE" };
                cell.DataType = new EnumValue<CellValues>(CellValues.String);
                cell.Append(cellValue);
                row.Append(cell);

                //CBM
                cell = new Cell() { CellReference = "G" + (start + i), StyleIndex = (UInt32Value)17U };
                cellValue = new CellValue { Text = "CBM" };
                cell.DataType = new EnumValue<CellValues>(CellValues.String);
                cell.Append(cellValue);
                row.Append(cell);

                //W
                cell = new Cell() { CellReference = "H" + (start + i), StyleIndex = (UInt32Value)17U };
                cellValue = new CellValue { Text = "W" };
                cell.DataType = new EnumValue<CellValues>(CellValues.String);
                cell.Append(cellValue);
                row.Append(cell);

                //D
                cell = new Cell() { CellReference = "I" + (start + i), StyleIndex = (UInt32Value)17U };
                cellValue = new CellValue { Text = "D" };
                cell.DataType = new EnumValue<CellValues>(CellValues.String);
                cell.Append(cellValue);
                row.Append(cell);

                //H
                cell = new Cell() { CellReference = "J" + (start + i), StyleIndex = (UInt32Value)17U };
                cellValue = new CellValue { Text = "H" };
                cell.DataType = new EnumValue<CellValues>(CellValues.String);
                cell.Append(cellValue);
                row.Append(cell);

                //QTY
                cell = new Cell() { CellReference = "K" + (start + i), StyleIndex = (UInt32Value)18U };
                cellValue = new CellValue { Text = "QTY" };
                cell.DataType = new EnumValue<CellValues>(CellValues.String);
                cell.Append(cellValue);
                row.Append(cell);

                //CARTON QTY
                cell = new Cell() { CellReference = "L" + (start + i), StyleIndex = (UInt32Value)18U };
                cellValue = new CellValue { Text = "CARTON QTY" };
                cell.DataType = new EnumValue<CellValues>(CellValues.String);
                cell.Append(cellValue);
                row.Append(cell);

                //TOTAL CBM
                cell = new Cell() { CellReference = "M" + (start + i), StyleIndex = (UInt32Value)18U };
                cellValue = new CellValue { Text = "TOTAL CBM" };
                cell.DataType = new EnumValue<CellValues>(CellValues.String);
                cell.Append(cellValue);
                row.Append(cell);

                //AMOUNT
                cell = new Cell() { CellReference = "N" + (start + i), StyleIndex = (UInt32Value)18U };
                cellValue = new CellValue { Text = "AMOUNT" };
                cell.DataType = new EnumValue<CellValues>(CellValues.String);
                cell.Append(cellValue);
                row.Append(cell);

                sheetData1.Append(row);
            }

            #endregion
        }

        private void RowsImage(Xdr.WorksheetDrawing worksheetDrawing1)
        {
            #region Quotation Items Images

            for (var i = 0; i < _quotationModel.QuotationItems.Count; i++)
            {
                Xdr.TwoCellAnchor twoCellAnchor = new Xdr.TwoCellAnchor() { EditAs = Xdr.EditAsValues.OneCell };

                Xdr.FromMarker fromMarker2 = new Xdr.FromMarker();
                Xdr.ColumnId columnId3 = new Xdr.ColumnId();
                columnId3.Text = "1";
                Xdr.ColumnOffset columnOffset3 = new Xdr.ColumnOffset();
                columnOffset3.Text = "76200";
                Xdr.RowId rowId3 = new Xdr.RowId();
                rowId3.Text = Convert.ToString(i + 6); //7
                Xdr.RowOffset rowOffset3 = new Xdr.RowOffset();
                rowOffset3.Text = "19050";

                fromMarker2.Append(columnId3);
                fromMarker2.Append(columnOffset3);
                fromMarker2.Append(rowId3);
                fromMarker2.Append(rowOffset3);

                Xdr.ToMarker toMarker2 = new Xdr.ToMarker();
                Xdr.ColumnId columnId4 = new Xdr.ColumnId();
                columnId4.Text = "1";
                Xdr.ColumnOffset columnOffset4 = new Xdr.ColumnOffset();
                columnOffset4.Text = "1657350";
                Xdr.RowId rowId4 = new Xdr.RowId();
                rowId4.Text = Convert.ToString(i + 6); //7
                Xdr.RowOffset rowOffset4 = new Xdr.RowOffset();
                rowOffset4.Text = "895350";

                toMarker2.Append(columnId4);
                toMarker2.Append(columnOffset4);
                toMarker2.Append(rowId4);
                toMarker2.Append(rowOffset4);

                Xdr.Picture picture2 = new Xdr.Picture();

                Xdr.NonVisualPictureProperties nonVisualPictureProperties2 = new Xdr.NonVisualPictureProperties();
                Xdr.NonVisualDrawingProperties nonVisualDrawingProperties2 = new Xdr.NonVisualDrawingProperties()
                {
                    Id = (UInt32Value)3U,
                    Name = "Picture 1112"
                };

                Xdr.NonVisualPictureDrawingProperties nonVisualPictureDrawingProperties2 =
                    new Xdr.NonVisualPictureDrawingProperties();
                A.PictureLocks pictureLocks2 = new A.PictureLocks() { NoChangeAspect = true, NoChangeArrowheads = true };

                nonVisualPictureDrawingProperties2.Append(pictureLocks2);

                nonVisualPictureProperties2.Append(nonVisualDrawingProperties2);
                nonVisualPictureProperties2.Append(nonVisualPictureDrawingProperties2);

                Xdr.BlipFill blipFill2 = new Xdr.BlipFill();

                A.Blip blip2 = new A.Blip() { Embed = "rId" + Convert.ToString(i + 6) /* "rId3" */ };
                blip2.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
                A.SourceRectangle sourceRectangle2 = new A.SourceRectangle();

                A.Stretch stretch2 = new A.Stretch();
                A.FillRectangle fillRectangle2 = new A.FillRectangle();

                stretch2.Append(fillRectangle2);

                blipFill2.Append(blip2);
                blipFill2.Append(sourceRectangle2);
                blipFill2.Append(stretch2);

                Xdr.ShapeProperties shapeProperties2 = new Xdr.ShapeProperties() { BlackWhiteMode = A.BlackWhiteModeValues.Auto };

                A.Transform2D transform2D2 = new A.Transform2D();
                A.Offset offset1 = new A.Offset() { X = 57150L, Y = 38100L };
                A.Extents extents1 = new A.Extents() { Cx = 2181225L, Cy = 1038225L };

                //A.Offset offset2 = new A.Offset() {X = 1152525L, Y = 2752725L};
                //A.Extents extents2 = new A.Extents() {Cx = 1581150L, Cy = 876300L};

                transform2D2.Append(offset1);
                transform2D2.Append(extents1);

                A.PresetGeometry presetGeometry2 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.Rectangle };
                A.AdjustValueList adjustValueList2 = new A.AdjustValueList();

                presetGeometry2.Append(adjustValueList2);
                A.NoFill noFill3 = new A.NoFill();

                A.Outline outline5 = new A.Outline() { Width = 1 };
                A.NoFill noFill4 = new A.NoFill();
                A.Miter miter2 = new A.Miter() { Limit = 800000 };
                A.HeadEnd headEnd2 = new A.HeadEnd();
                A.TailEnd tailEnd2 = new A.TailEnd();

                outline5.Append(noFill4);
                outline5.Append(miter2);
                outline5.Append(headEnd2);
                outline5.Append(tailEnd2);

                shapeProperties2.Append(transform2D2);
                shapeProperties2.Append(presetGeometry2);
                shapeProperties2.Append(noFill3);
                shapeProperties2.Append(outline5);

                picture2.Append(nonVisualPictureProperties2);
                picture2.Append(blipFill2);
                picture2.Append(shapeProperties2);
                Xdr.ClientData clientData2 = new Xdr.ClientData();

                twoCellAnchor.Append(fromMarker2);
                twoCellAnchor.Append(toMarker2);
                twoCellAnchor.Append(picture2);
                twoCellAnchor.Append(clientData2);


                worksheetDrawing1.Append(twoCellAnchor);
            }

            #endregion
        }
        #endregion
        

        // Generates content of extendedFilePropertiesPart1.
        private void GenerateExtendedFilePropertiesPart1Content(ExtendedFilePropertiesPart extendedFilePropertiesPart1)
        {
            Ap.Properties properties1 = new Ap.Properties();
            properties1.AddNamespaceDeclaration("vt", "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes");
            Ap.Application application1 = new Ap.Application();
            application1.Text = "Microsoft Excel";
            Ap.DocumentSecurity documentSecurity1 = new Ap.DocumentSecurity();
            documentSecurity1.Text = "0";
            Ap.ScaleCrop scaleCrop1 = new Ap.ScaleCrop();
            scaleCrop1.Text = "false";

            Ap.HeadingPairs headingPairs1 = new Ap.HeadingPairs();

            Vt.VTVector vTVector1 = new Vt.VTVector() { BaseType = Vt.VectorBaseValues.Variant, Size = (UInt32Value)2U };

            Vt.Variant variant1 = new Vt.Variant();
            Vt.VTLPSTR vTLPSTR1 = new Vt.VTLPSTR();
            vTLPSTR1.Text = "Worksheets";

            variant1.Append(vTLPSTR1);

            Vt.Variant variant2 = new Vt.Variant();
            Vt.VTInt32 vTInt321 = new Vt.VTInt32();
            vTInt321.Text = "1";

            variant2.Append(vTInt321);

            vTVector1.Append(variant1);
            vTVector1.Append(variant2);

            headingPairs1.Append(vTVector1);

            Ap.TitlesOfParts titlesOfParts1 = new Ap.TitlesOfParts();

            Vt.VTVector vTVector2 = new Vt.VTVector() { BaseType = Vt.VectorBaseValues.Lpstr, Size = (UInt32Value)1U };
            Vt.VTLPSTR vTLPSTR2 = new Vt.VTLPSTR();
            vTLPSTR2.Text = "Quotation";

            vTVector2.Append(vTLPSTR2);

            titlesOfParts1.Append(vTVector2);
            Ap.LinksUpToDate linksUpToDate1 = new Ap.LinksUpToDate();
            linksUpToDate1.Text = "false";
            Ap.SharedDocument sharedDocument1 = new Ap.SharedDocument();
            sharedDocument1.Text = "false";
            Ap.HyperlinksChanged hyperlinksChanged1 = new Ap.HyperlinksChanged();
            hyperlinksChanged1.Text = "false";
            Ap.ApplicationVersion applicationVersion1 = new Ap.ApplicationVersion();
            applicationVersion1.Text = "15.0300";

            properties1.Append(application1);
            properties1.Append(documentSecurity1);
            properties1.Append(scaleCrop1);
            properties1.Append(headingPairs1);
            properties1.Append(titlesOfParts1);
            properties1.Append(linksUpToDate1);
            properties1.Append(sharedDocument1);
            properties1.Append(hyperlinksChanged1);
            properties1.Append(applicationVersion1);

            extendedFilePropertiesPart1.Properties = properties1;
        }

        // Generates content of workbookPart1.
        private void GenerateWorkbookPart1Content(WorkbookPart workbookPart1)
        {
            Workbook workbook1 = new Workbook() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "x15" } };
            workbook1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            workbook1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            workbook1.AddNamespaceDeclaration("x15", "http://schemas.microsoft.com/office/spreadsheetml/2010/11/main");
            FileVersion fileVersion1 = new FileVersion() { ApplicationName = "xl", LastEdited = "6", LowestEdited = "4", BuildVersion = "14420" };
            WorkbookProperties workbookProperties1 = new WorkbookProperties() { DefaultThemeVersion = (UInt32Value)124226U };

            AlternateContent alternateContent1 = new AlternateContent();
            alternateContent1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");

            AlternateContentChoice alternateContentChoice1 = new AlternateContentChoice() { Requires = "x15" };

            X15ac.AbsolutePath absolutePath1 = new X15ac.AbsolutePath() { Url = "C:\\Users\\devli\\Desktop\\" };
            absolutePath1.AddNamespaceDeclaration("x15ac", "http://schemas.microsoft.com/office/spreadsheetml/2010/11/ac");

            alternateContentChoice1.Append(absolutePath1);

            alternateContent1.Append(alternateContentChoice1);

            BookViews bookViews1 = new BookViews();
            WorkbookView workbookView1 = new WorkbookView() { XWindow = 0, YWindow = 0, WindowWidth = (UInt32Value)15600U, WindowHeight = (UInt32Value)11760U };

            bookViews1.Append(workbookView1);

            Sheets sheets1 = new Sheets();
            Sheet sheet1 = new Sheet() { Name = "Quotation", SheetId = (UInt32Value)1U, Id = "rId1" };

            sheets1.Append(sheet1);
            CalculationProperties calculationProperties1 = new CalculationProperties() { CalculationId = (UInt32Value)124519U };

            workbook1.Append(fileVersion1);
            workbook1.Append(workbookProperties1);
            workbook1.Append(alternateContent1);
            workbook1.Append(bookViews1);
            workbook1.Append(sheets1);
            workbook1.Append(calculationProperties1);

            workbookPart1.Workbook = workbook1;
        }

        // Generates content of workbookStylesPart1.
        private void GenerateWorkbookStylesPart1Content(WorkbookStylesPart workbookStylesPart1)
        {
            Stylesheet stylesheet1 = new Stylesheet() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "x14ac" } };
            stylesheet1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            stylesheet1.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");

            Fonts fonts1 = new Fonts() { Count = (UInt32Value)17U, KnownFonts = true };

            Font font1 = new Font();
            FontSize fontSize1 = new FontSize() { Val = 11D };
            DocumentFormat.OpenXml.Spreadsheet.Color color1 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Theme = (UInt32Value)1U };
            FontName fontName1 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering1 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme1 = new FontScheme() { Val = FontSchemeValues.Minor };

            font1.Append(fontSize1);
            font1.Append(color1);
            font1.Append(fontName1);
            font1.Append(fontFamilyNumbering1);
            font1.Append(fontScheme1);

            Font font2 = new Font();
            Bold bold1 = new Bold();
            FontSize fontSize2 = new FontSize() { Val = 11D };
            DocumentFormat.OpenXml.Spreadsheet.Color color2 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Theme = (UInt32Value)1U };
            FontName fontName2 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering2 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme2 = new FontScheme() { Val = FontSchemeValues.Minor };

            font2.Append(bold1);
            font2.Append(fontSize2);
            font2.Append(color2);
            font2.Append(fontName2);
            font2.Append(fontFamilyNumbering2);
            font2.Append(fontScheme2);

            Font font3 = new Font();
            Bold bold2 = new Bold();
            FontSize fontSize3 = new FontSize() { Val = 20D };
            DocumentFormat.OpenXml.Spreadsheet.Color color3 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Rgb = "FFA47900" };
            FontName fontName3 = new FontName() { Val = "Brush Script MT" };
            FontFamilyNumbering fontFamilyNumbering3 = new FontFamilyNumbering() { Val = 4 };

            font3.Append(bold2);
            font3.Append(fontSize3);
            font3.Append(color3);
            font3.Append(fontName3);
            font3.Append(fontFamilyNumbering3);

            Font font4 = new Font();
            Bold bold3 = new Bold();
            FontSize fontSize4 = new FontSize() { Val = 24D };
            DocumentFormat.OpenXml.Spreadsheet.Color color4 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Rgb = "FFA47900" };
            FontName fontName4 = new FontName() { Val = "Brush Script MT" };
            FontFamilyNumbering fontFamilyNumbering4 = new FontFamilyNumbering() { Val = 4 };

            font4.Append(bold3);
            font4.Append(fontSize4);
            font4.Append(color4);
            font4.Append(fontName4);
            font4.Append(fontFamilyNumbering4);

            Font font5 = new Font();
            FontSize fontSize5 = new FontSize() { Val = 12D };
            DocumentFormat.OpenXml.Spreadsheet.Color color5 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Theme = (UInt32Value)1U };
            FontName fontName5 = new FontName() { Val = "Arial" };
            FontFamilyNumbering fontFamilyNumbering5 = new FontFamilyNumbering() { Val = 2 };

            font5.Append(fontSize5);
            font5.Append(color5);
            font5.Append(fontName5);
            font5.Append(fontFamilyNumbering5);

            Font font6 = new Font();
            FontSize fontSize6 = new FontSize() { Val = 10D };
            DocumentFormat.OpenXml.Spreadsheet.Color color6 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Theme = (UInt32Value)1U };
            FontName fontName6 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering6 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme3 = new FontScheme() { Val = FontSchemeValues.Minor };

            font6.Append(fontSize6);
            font6.Append(color6);
            font6.Append(fontName6);
            font6.Append(fontFamilyNumbering6);
            font6.Append(fontScheme3);

            Font font7 = new Font();
            FontSize fontSize7 = new FontSize() { Val = 11D };
            DocumentFormat.OpenXml.Spreadsheet.Color color7 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Theme = (UInt32Value)1U };
            FontName fontName7 = new FontName() { Val = "Arial" };
            FontFamilyNumbering fontFamilyNumbering7 = new FontFamilyNumbering() { Val = 2 };

            font7.Append(fontSize7);
            font7.Append(color7);
            font7.Append(fontName7);
            font7.Append(fontFamilyNumbering7);

            Font font8 = new Font();
            Bold bold4 = new Bold();
            FontSize fontSize8 = new FontSize() { Val = 11D };
            DocumentFormat.OpenXml.Spreadsheet.Color color8 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Theme = (UInt32Value)1U };
            FontName fontName8 = new FontName() { Val = "Arial" };
            FontFamilyNumbering fontFamilyNumbering8 = new FontFamilyNumbering() { Val = 2 };

            font8.Append(bold4);
            font8.Append(fontSize8);
            font8.Append(color8);
            font8.Append(fontName8);
            font8.Append(fontFamilyNumbering8);

            Font font9 = new Font();
            Bold bold5 = new Bold();
            FontSize fontSize9 = new FontSize() { Val = 10D };
            FontName fontName9 = new FontName() { Val = "Arial" };
            FontFamilyNumbering fontFamilyNumbering9 = new FontFamilyNumbering() { Val = 2 };

            font9.Append(bold5);
            font9.Append(fontSize9);
            font9.Append(fontName9);
            font9.Append(fontFamilyNumbering9);

            Font font10 = new Font();
            Bold bold6 = new Bold();
            FontSize fontSize10 = new FontSize() { Val = 8D };
            DocumentFormat.OpenXml.Spreadsheet.Color color9 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Rgb = "FFFF0000" };
            FontName fontName10 = new FontName() { Val = "Arial" };
            FontFamilyNumbering fontFamilyNumbering10 = new FontFamilyNumbering() { Val = 2 };

            font10.Append(bold6);
            font10.Append(fontSize10);
            font10.Append(color9);
            font10.Append(fontName10);
            font10.Append(fontFamilyNumbering10);

            Font font11 = new Font();
            Bold bold7 = new Bold();
            FontSize fontSize11 = new FontSize() { Val = 11D };
            DocumentFormat.OpenXml.Spreadsheet.Color color10 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Rgb = "FFFF0000" };
            FontName fontName11 = new FontName() { Val = "Arial" };
            FontFamilyNumbering fontFamilyNumbering11 = new FontFamilyNumbering() { Val = 2 };

            font11.Append(bold7);
            font11.Append(fontSize11);
            font11.Append(color10);
            font11.Append(fontName11);
            font11.Append(fontFamilyNumbering11);

            Font font12 = new Font();
            Bold bold8 = new Bold();
            FontSize fontSize12 = new FontSize() { Val = 24D };
            DocumentFormat.OpenXml.Spreadsheet.Color color11 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Theme = (UInt32Value)1U };
            FontName fontName12 = new FontName() { Val = "Arial" };
            FontFamilyNumbering fontFamilyNumbering12 = new FontFamilyNumbering() { Val = 2 };

            font12.Append(bold8);
            font12.Append(fontSize12);
            font12.Append(color11);
            font12.Append(fontName12);
            font12.Append(fontFamilyNumbering12);

            Font font13 = new Font();
            FontSize fontSize13 = new FontSize() { Val = 10D };
            DocumentFormat.OpenXml.Spreadsheet.Color color12 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Theme = (UInt32Value)1U };
            FontName fontName13 = new FontName() { Val = "Arial" };
            FontFamilyNumbering fontFamilyNumbering13 = new FontFamilyNumbering() { Val = 2 };
            FontCharSet fontCharSet1 = new FontCharSet() { Val = 163 };

            font13.Append(fontSize13);
            font13.Append(color12);
            font13.Append(fontName13);
            font13.Append(fontFamilyNumbering13);
            font13.Append(fontCharSet1);

            Font font14 = new Font();
            Bold bold9 = new Bold();
            FontSize fontSize14 = new FontSize() { Val = 10D };
            DocumentFormat.OpenXml.Spreadsheet.Color color13 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Theme = (UInt32Value)1U };
            FontName fontName14 = new FontName() { Val = "Arial" };
            FontFamilyNumbering fontFamilyNumbering14 = new FontFamilyNumbering() { Val = 2 };
            FontCharSet fontCharSet2 = new FontCharSet() { Val = 163 };

            font14.Append(bold9);
            font14.Append(fontSize14);
            font14.Append(color13);
            font14.Append(fontName14);
            font14.Append(fontFamilyNumbering14);
            font14.Append(fontCharSet2);

            Font font15 = new Font();
            Bold bold10 = new Bold();
            FontSize fontSize15 = new FontSize() { Val = 10D };
            FontName fontName15 = new FontName() { Val = "Arial" };
            FontFamilyNumbering fontFamilyNumbering15 = new FontFamilyNumbering() { Val = 2 };
            FontCharSet fontCharSet3 = new FontCharSet() { Val = 163 };

            font15.Append(bold10);
            font15.Append(fontSize15);
            font15.Append(fontName15);
            font15.Append(fontFamilyNumbering15);
            font15.Append(fontCharSet3);

            Font font16 = new Font();
            FontSize fontSize16 = new FontSize() { Val = 10D };
            FontName fontName16 = new FontName() { Val = "Arial" };
            FontFamilyNumbering fontFamilyNumbering16 = new FontFamilyNumbering() { Val = 2 };
            FontCharSet fontCharSet4 = new FontCharSet() { Val = 163 };

            font16.Append(fontSize16);
            font16.Append(fontName16);
            font16.Append(fontFamilyNumbering16);
            font16.Append(fontCharSet4);

            Font font17 = new Font();
            Bold bold11 = new Bold();
            FontSize fontSize17 = new FontSize() { Val = 10D };
            DocumentFormat.OpenXml.Spreadsheet.Color color14 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Rgb = "FFFF0000" };
            FontName fontName17 = new FontName() { Val = "Arial" };
            FontFamilyNumbering fontFamilyNumbering17 = new FontFamilyNumbering() { Val = 2 };
            FontCharSet fontCharSet5 = new FontCharSet() { Val = 163 };

            font17.Append(bold11);
            font17.Append(fontSize17);
            font17.Append(color14);
            font17.Append(fontName17);
            font17.Append(fontFamilyNumbering17);
            font17.Append(fontCharSet5);

            fonts1.Append(font1);
            fonts1.Append(font2);
            fonts1.Append(font3);
            fonts1.Append(font4);
            fonts1.Append(font5);
            fonts1.Append(font6);
            fonts1.Append(font7);
            fonts1.Append(font8);
            fonts1.Append(font9);
            fonts1.Append(font10);
            fonts1.Append(font11);
            fonts1.Append(font12);
            fonts1.Append(font13);
            fonts1.Append(font14);
            fonts1.Append(font15);
            fonts1.Append(font16);
            fonts1.Append(font17);

            Fills fills1 = new Fills() { Count = (UInt32Value)4U };

            Fill fill1 = new Fill();
            PatternFill patternFill1 = new PatternFill() { PatternType = PatternValues.None };

            fill1.Append(patternFill1);

            Fill fill2 = new Fill();
            PatternFill patternFill2 = new PatternFill() { PatternType = PatternValues.Gray125 };

            fill2.Append(patternFill2);

            Fill fill3 = new Fill();

            PatternFill patternFill3 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor1 = new ForegroundColor() { Theme = (UInt32Value)0U };
            BackgroundColor backgroundColor1 = new BackgroundColor() { Indexed = (UInt32Value)64U };

            patternFill3.Append(foregroundColor1);
            patternFill3.Append(backgroundColor1);

            fill3.Append(patternFill3);

            Fill fill4 = new Fill();

            PatternFill patternFill4 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor2 = new ForegroundColor() { Theme = (UInt32Value)6U, Tint = 0.39997558519241921D };
            BackgroundColor backgroundColor2 = new BackgroundColor() { Indexed = (UInt32Value)64U };

            patternFill4.Append(foregroundColor2);
            patternFill4.Append(backgroundColor2);

            fill4.Append(patternFill4);

            fills1.Append(fill1);
            fills1.Append(fill2);
            fills1.Append(fill3);
            fills1.Append(fill4);

            Borders borders1 = new Borders() { Count = (UInt32Value)9U };

            Border border1 = new Border();
            LeftBorder leftBorder1 = new LeftBorder();
            RightBorder rightBorder1 = new RightBorder();
            TopBorder topBorder1 = new TopBorder();
            BottomBorder bottomBorder1 = new BottomBorder();
            DiagonalBorder diagonalBorder1 = new DiagonalBorder();

            border1.Append(leftBorder1);
            border1.Append(rightBorder1);
            border1.Append(topBorder1);
            border1.Append(bottomBorder1);
            border1.Append(diagonalBorder1);

            Border border2 = new Border();

            LeftBorder leftBorder2 = new LeftBorder() { Style = BorderStyleValues.Thin };
            DocumentFormat.OpenXml.Spreadsheet.Color color15 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };

            leftBorder2.Append(color15);

            RightBorder rightBorder2 = new RightBorder() { Style = BorderStyleValues.Thin };
            DocumentFormat.OpenXml.Spreadsheet.Color color16 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };

            rightBorder2.Append(color16);

            TopBorder topBorder2 = new TopBorder() { Style = BorderStyleValues.Thin };
            DocumentFormat.OpenXml.Spreadsheet.Color color17 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };

            topBorder2.Append(color17);

            BottomBorder bottomBorder2 = new BottomBorder() { Style = BorderStyleValues.Thin };
            DocumentFormat.OpenXml.Spreadsheet.Color color18 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };

            bottomBorder2.Append(color18);
            DiagonalBorder diagonalBorder2 = new DiagonalBorder();

            border2.Append(leftBorder2);
            border2.Append(rightBorder2);
            border2.Append(topBorder2);
            border2.Append(bottomBorder2);
            border2.Append(diagonalBorder2);

            Border border3 = new Border();

            LeftBorder leftBorder3 = new LeftBorder() { Style = BorderStyleValues.Thin };
            DocumentFormat.OpenXml.Spreadsheet.Color color19 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };

            leftBorder3.Append(color19);

            RightBorder rightBorder3 = new RightBorder() { Style = BorderStyleValues.Thin };
            DocumentFormat.OpenXml.Spreadsheet.Color color20 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };

            rightBorder3.Append(color20);

            TopBorder topBorder3 = new TopBorder() { Style = BorderStyleValues.Thin };
            DocumentFormat.OpenXml.Spreadsheet.Color color21 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };

            topBorder3.Append(color21);
            BottomBorder bottomBorder3 = new BottomBorder();
            DiagonalBorder diagonalBorder3 = new DiagonalBorder();

            border3.Append(leftBorder3);
            border3.Append(rightBorder3);
            border3.Append(topBorder3);
            border3.Append(bottomBorder3);
            border3.Append(diagonalBorder3);

            Border border4 = new Border();

            LeftBorder leftBorder4 = new LeftBorder() { Style = BorderStyleValues.Thin };
            DocumentFormat.OpenXml.Spreadsheet.Color color22 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };

            leftBorder4.Append(color22);
            RightBorder rightBorder4 = new RightBorder();

            TopBorder topBorder4 = new TopBorder() { Style = BorderStyleValues.Thin };
            DocumentFormat.OpenXml.Spreadsheet.Color color23 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };

            topBorder4.Append(color23);

            BottomBorder bottomBorder4 = new BottomBorder() { Style = BorderStyleValues.Thin };
            DocumentFormat.OpenXml.Spreadsheet.Color color24 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };

            bottomBorder4.Append(color24);
            DiagonalBorder diagonalBorder4 = new DiagonalBorder();

            border4.Append(leftBorder4);
            border4.Append(rightBorder4);
            border4.Append(topBorder4);
            border4.Append(bottomBorder4);
            border4.Append(diagonalBorder4);

            Border border5 = new Border();
            LeftBorder leftBorder5 = new LeftBorder();

            RightBorder rightBorder5 = new RightBorder() { Style = BorderStyleValues.Thin };
            DocumentFormat.OpenXml.Spreadsheet.Color color25 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };

            rightBorder5.Append(color25);

            TopBorder topBorder5 = new TopBorder() { Style = BorderStyleValues.Thin };
            DocumentFormat.OpenXml.Spreadsheet.Color color26 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };

            topBorder5.Append(color26);

            BottomBorder bottomBorder5 = new BottomBorder() { Style = BorderStyleValues.Thin };
            DocumentFormat.OpenXml.Spreadsheet.Color color27 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };

            bottomBorder5.Append(color27);
            DiagonalBorder diagonalBorder5 = new DiagonalBorder();

            border5.Append(leftBorder5);
            border5.Append(rightBorder5);
            border5.Append(topBorder5);
            border5.Append(bottomBorder5);
            border5.Append(diagonalBorder5);

            Border border6 = new Border();
            LeftBorder leftBorder6 = new LeftBorder();
            RightBorder rightBorder6 = new RightBorder();

            TopBorder topBorder6 = new TopBorder() { Style = BorderStyleValues.Thin };
            DocumentFormat.OpenXml.Spreadsheet.Color color28 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };

            topBorder6.Append(color28);

            BottomBorder bottomBorder6 = new BottomBorder() { Style = BorderStyleValues.Thin };
            DocumentFormat.OpenXml.Spreadsheet.Color color29 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };

            bottomBorder6.Append(color29);
            DiagonalBorder diagonalBorder6 = new DiagonalBorder();

            border6.Append(leftBorder6);
            border6.Append(rightBorder6);
            border6.Append(topBorder6);
            border6.Append(bottomBorder6);
            border6.Append(diagonalBorder6);

            Border border7 = new Border();

            LeftBorder leftBorder7 = new LeftBorder() { Style = BorderStyleValues.Thin };
            DocumentFormat.OpenXml.Spreadsheet.Color color30 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };

            leftBorder7.Append(color30);

            RightBorder rightBorder7 = new RightBorder() { Style = BorderStyleValues.Thin };
            DocumentFormat.OpenXml.Spreadsheet.Color color31 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };

            rightBorder7.Append(color31);
            TopBorder topBorder7 = new TopBorder();

            BottomBorder bottomBorder7 = new BottomBorder() { Style = BorderStyleValues.Thin };
            DocumentFormat.OpenXml.Spreadsheet.Color color32 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };

            bottomBorder7.Append(color32);
            DiagonalBorder diagonalBorder7 = new DiagonalBorder();

            border7.Append(leftBorder7);
            border7.Append(rightBorder7);
            border7.Append(topBorder7);
            border7.Append(bottomBorder7);
            border7.Append(diagonalBorder7);

            Border border8 = new Border();

            LeftBorder leftBorder8 = new LeftBorder() { Style = BorderStyleValues.Thin };
            DocumentFormat.OpenXml.Spreadsheet.Color color33 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };

            leftBorder8.Append(color33);
            RightBorder rightBorder8 = new RightBorder();

            TopBorder topBorder8 = new TopBorder() { Style = BorderStyleValues.Thin };
            DocumentFormat.OpenXml.Spreadsheet.Color color34 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };

            topBorder8.Append(color34);
            BottomBorder bottomBorder8 = new BottomBorder();
            DiagonalBorder diagonalBorder8 = new DiagonalBorder();

            border8.Append(leftBorder8);
            border8.Append(rightBorder8);
            border8.Append(topBorder8);
            border8.Append(bottomBorder8);
            border8.Append(diagonalBorder8);

            Border border9 = new Border();
            LeftBorder leftBorder9 = new LeftBorder();

            RightBorder rightBorder9 = new RightBorder() { Style = BorderStyleValues.Thin };
            DocumentFormat.OpenXml.Spreadsheet.Color color35 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };

            rightBorder9.Append(color35);

            TopBorder topBorder9 = new TopBorder() { Style = BorderStyleValues.Thin };
            DocumentFormat.OpenXml.Spreadsheet.Color color36 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };

            topBorder9.Append(color36);
            BottomBorder bottomBorder9 = new BottomBorder();
            DiagonalBorder diagonalBorder9 = new DiagonalBorder();

            border9.Append(leftBorder9);
            border9.Append(rightBorder9);
            border9.Append(topBorder9);
            border9.Append(bottomBorder9);
            border9.Append(diagonalBorder9);

            borders1.Append(border1);
            borders1.Append(border2);
            borders1.Append(border3);
            borders1.Append(border4);
            borders1.Append(border5);
            borders1.Append(border6);
            borders1.Append(border7);
            borders1.Append(border8);
            borders1.Append(border9);

            CellStyleFormats cellStyleFormats1 = new CellStyleFormats() { Count = (UInt32Value)1U };
            CellFormat cellFormat1 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U };

            cellStyleFormats1.Append(cellFormat1);

            CellFormats cellFormats1 = new CellFormats() { Count = (UInt32Value)40U };
            CellFormat cellFormat2 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U };
            CellFormat cellFormat3 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)1U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true };
            CellFormat cellFormat4 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true };
            CellFormat cellFormat5 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)5U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true };

            CellFormat cellFormat6 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment1 = new Alignment() { Vertical = VerticalAlignmentValues.Top, WrapText = true };

            cellFormat6.Append(alignment1);

            CellFormat cellFormat7 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)7U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)1U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment2 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat7.Append(alignment2);

            CellFormat cellFormat8 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)7U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)2U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment3 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat8.Append(alignment3);

            CellFormat cellFormat9 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)7U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment4 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat9.Append(alignment4);

            CellFormat cellFormat10 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)9U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment5 = new Alignment() { Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat10.Append(alignment5);

            CellFormat cellFormat11 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)9U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)2U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment6 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat11.Append(alignment6);

            CellFormat cellFormat12 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)14U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)1U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment7 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat12.Append(alignment7);

            CellFormat cellFormat13 = new CellFormat() { NumberFormatId = (UInt32Value)1U, FontId = (UInt32Value)14U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)1U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment8 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat13.Append(alignment8);

            CellFormat cellFormat14 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)15U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)1U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment9 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat14.Append(alignment9);

            CellFormat cellFormat15 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)15U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)6U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment10 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat15.Append(alignment10);
            CellFormat cellFormat16 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)12U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)1U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyBorder = true };

            CellFormat cellFormat17 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)16U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)6U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment11 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat17.Append(alignment11);

            CellFormat cellFormat18 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)16U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)1U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment12 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat18.Append(alignment12);

            CellFormat cellFormat19 = new CellFormat() { NumberFormatId = (UInt32Value)1U, FontId = (UInt32Value)16U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)1U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment13 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat19.Append(alignment13);

            CellFormat cellFormat20 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)16U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)1U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment14 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat20.Append(alignment14);

            CellFormat cellFormat21 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)13U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)2U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment15 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat21.Append(alignment15);

            CellFormat cellFormat22 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)13U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)6U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment16 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat22.Append(alignment16);

            CellFormat cellFormat23 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)13U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)2U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment17 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat23.Append(alignment17);

            CellFormat cellFormat24 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)13U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)6U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment18 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat24.Append(alignment18);

            CellFormat cellFormat25 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)9U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)1U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment19 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat25.Append(alignment19);

            CellFormat cellFormat26 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment20 = new Alignment() { Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Top, WrapText = true };

            cellFormat26.Append(alignment20);

            CellFormat cellFormat27 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)10U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)3U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment21 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat27.Append(alignment21);

            CellFormat cellFormat28 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)10U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)4U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment22 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat28.Append(alignment22);

            CellFormat cellFormat29 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)10U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)7U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment23 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat29.Append(alignment23);

            CellFormat cellFormat30 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)10U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)8U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment24 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat30.Append(alignment24);

            CellFormat cellFormat31 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)11U, FillId = (UInt32Value)3U, BorderId = (UInt32Value)1U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment25 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center };

            cellFormat31.Append(alignment25);

            CellFormat cellFormat32 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)14U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)1U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment26 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat32.Append(alignment26);

            CellFormat cellFormat33 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)14U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)3U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment27 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat33.Append(alignment27);

            CellFormat cellFormat34 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)14U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)4U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment28 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat34.Append(alignment28);

            CellFormat cellFormat35 = new CellFormat() { NumberFormatId = (UInt32Value)1U, FontId = (UInt32Value)14U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)3U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment29 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat35.Append(alignment29);

            CellFormat cellFormat36 = new CellFormat() { NumberFormatId = (UInt32Value)1U, FontId = (UInt32Value)14U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)5U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment30 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat36.Append(alignment30);

            CellFormat cellFormat37 = new CellFormat() { NumberFormatId = (UInt32Value)1U, FontId = (UInt32Value)14U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)4U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment31 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat37.Append(alignment31);

            CellFormat cellFormat38 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)14U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)2U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment32 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat38.Append(alignment32);

            CellFormat cellFormat39 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)14U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)6U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment33 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat39.Append(alignment33);

            CellFormat cellFormat40 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)8U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)3U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment34 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat40.Append(alignment34);

            CellFormat cellFormat41 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)8U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)5U, FormatId = (UInt32Value)0U, ApplyFont = true, ApplyBorder = true, ApplyAlignment = true };
            Alignment alignment35 = new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true };

            cellFormat41.Append(alignment35);

            cellFormats1.Append(cellFormat2);
            cellFormats1.Append(cellFormat3);
            cellFormats1.Append(cellFormat4);
            cellFormats1.Append(cellFormat5);
            cellFormats1.Append(cellFormat6);
            cellFormats1.Append(cellFormat7);
            cellFormats1.Append(cellFormat8);
            cellFormats1.Append(cellFormat9);
            cellFormats1.Append(cellFormat10);
            cellFormats1.Append(cellFormat11);
            cellFormats1.Append(cellFormat12);
            cellFormats1.Append(cellFormat13);
            cellFormats1.Append(cellFormat14);
            cellFormats1.Append(cellFormat15);
            cellFormats1.Append(cellFormat16);
            cellFormats1.Append(cellFormat17);
            cellFormats1.Append(cellFormat18);
            cellFormats1.Append(cellFormat19);
            cellFormats1.Append(cellFormat20);
            cellFormats1.Append(cellFormat21);
            cellFormats1.Append(cellFormat22);
            cellFormats1.Append(cellFormat23);
            cellFormats1.Append(cellFormat24);
            cellFormats1.Append(cellFormat25);
            cellFormats1.Append(cellFormat26);
            cellFormats1.Append(cellFormat27);
            cellFormats1.Append(cellFormat28);
            cellFormats1.Append(cellFormat29);
            cellFormats1.Append(cellFormat30);
            cellFormats1.Append(cellFormat31);
            cellFormats1.Append(cellFormat32);
            cellFormats1.Append(cellFormat33);
            cellFormats1.Append(cellFormat34);
            cellFormats1.Append(cellFormat35);
            cellFormats1.Append(cellFormat36);
            cellFormats1.Append(cellFormat37);
            cellFormats1.Append(cellFormat38);
            cellFormats1.Append(cellFormat39);
            cellFormats1.Append(cellFormat40);
            cellFormats1.Append(cellFormat41);

            CellStyles cellStyles1 = new CellStyles() { Count = (UInt32Value)1U };
            CellStyle cellStyle1 = new CellStyle() { Name = "Normal", FormatId = (UInt32Value)0U, BuiltinId = (UInt32Value)0U };

            cellStyles1.Append(cellStyle1);
            DifferentialFormats differentialFormats1 = new DifferentialFormats() { Count = (UInt32Value)0U };
            TableStyles tableStyles1 = new TableStyles() { Count = (UInt32Value)0U, DefaultTableStyle = "TableStyleMedium9", DefaultPivotStyle = "PivotStyleLight16" };

            StylesheetExtensionList stylesheetExtensionList1 = new StylesheetExtensionList();

            StylesheetExtension stylesheetExtension1 = new StylesheetExtension() { Uri = "{EB79DEF2-80B8-43e5-95BD-54CBDDF9020C}" };
            stylesheetExtension1.AddNamespaceDeclaration("x14", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
            X14.SlicerStyles slicerStyles1 = new X14.SlicerStyles() { DefaultSlicerStyle = "SlicerStyleLight1" };

            stylesheetExtension1.Append(slicerStyles1);

            StylesheetExtension stylesheetExtension2 = new StylesheetExtension() { Uri = "{9260A510-F301-46a8-8635-F512D64BE5F5}" };
            stylesheetExtension2.AddNamespaceDeclaration("x15", "http://schemas.microsoft.com/office/spreadsheetml/2010/11/main");
            X15.TimelineStyles timelineStyles1 = new X15.TimelineStyles() { DefaultTimelineStyle = "TimeSlicerStyleLight1" };

            stylesheetExtension2.Append(timelineStyles1);

            stylesheetExtensionList1.Append(stylesheetExtension1);
            stylesheetExtensionList1.Append(stylesheetExtension2);

            stylesheet1.Append(fonts1);
            stylesheet1.Append(fills1);
            stylesheet1.Append(borders1);
            stylesheet1.Append(cellStyleFormats1);
            stylesheet1.Append(cellFormats1);
            stylesheet1.Append(cellStyles1);
            stylesheet1.Append(differentialFormats1);
            stylesheet1.Append(tableStyles1);
            stylesheet1.Append(stylesheetExtensionList1);

            workbookStylesPart1.Stylesheet = stylesheet1;
        }

        // Generates content of themePart1.
        private void GenerateThemePart1Content(ThemePart themePart1)
        {
            A.Theme theme1 = new A.Theme() { Name = "Office Theme" };
            theme1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

            A.ThemeElements themeElements1 = new A.ThemeElements();

            A.ColorScheme colorScheme1 = new A.ColorScheme() { Name = "Office" };

            A.Dark1Color dark1Color1 = new A.Dark1Color();
            A.SystemColor systemColor1 = new A.SystemColor() { Val = A.SystemColorValues.WindowText, LastColor = "000000" };

            dark1Color1.Append(systemColor1);

            A.Light1Color light1Color1 = new A.Light1Color();
            A.SystemColor systemColor2 = new A.SystemColor() { Val = A.SystemColorValues.Window, LastColor = "FFFFFF" };

            light1Color1.Append(systemColor2);

            A.Dark2Color dark2Color1 = new A.Dark2Color();
            A.RgbColorModelHex rgbColorModelHex1 = new A.RgbColorModelHex() { Val = "1F497D" };

            dark2Color1.Append(rgbColorModelHex1);

            A.Light2Color light2Color1 = new A.Light2Color();
            A.RgbColorModelHex rgbColorModelHex2 = new A.RgbColorModelHex() { Val = "EEECE1" };

            light2Color1.Append(rgbColorModelHex2);

            A.Accent1Color accent1Color1 = new A.Accent1Color();
            A.RgbColorModelHex rgbColorModelHex3 = new A.RgbColorModelHex() { Val = "4F81BD" };

            accent1Color1.Append(rgbColorModelHex3);

            A.Accent2Color accent2Color1 = new A.Accent2Color();
            A.RgbColorModelHex rgbColorModelHex4 = new A.RgbColorModelHex() { Val = "C0504D" };

            accent2Color1.Append(rgbColorModelHex4);

            A.Accent3Color accent3Color1 = new A.Accent3Color();
            A.RgbColorModelHex rgbColorModelHex5 = new A.RgbColorModelHex() { Val = "9BBB59" };

            accent3Color1.Append(rgbColorModelHex5);

            A.Accent4Color accent4Color1 = new A.Accent4Color();
            A.RgbColorModelHex rgbColorModelHex6 = new A.RgbColorModelHex() { Val = "8064A2" };

            accent4Color1.Append(rgbColorModelHex6);

            A.Accent5Color accent5Color1 = new A.Accent5Color();
            A.RgbColorModelHex rgbColorModelHex7 = new A.RgbColorModelHex() { Val = "4BACC6" };

            accent5Color1.Append(rgbColorModelHex7);

            A.Accent6Color accent6Color1 = new A.Accent6Color();
            A.RgbColorModelHex rgbColorModelHex8 = new A.RgbColorModelHex() { Val = "F79646" };

            accent6Color1.Append(rgbColorModelHex8);

            A.Hyperlink hyperlink1 = new A.Hyperlink();
            A.RgbColorModelHex rgbColorModelHex9 = new A.RgbColorModelHex() { Val = "0000FF" };

            hyperlink1.Append(rgbColorModelHex9);

            A.FollowedHyperlinkColor followedHyperlinkColor1 = new A.FollowedHyperlinkColor();
            A.RgbColorModelHex rgbColorModelHex10 = new A.RgbColorModelHex() { Val = "800080" };

            followedHyperlinkColor1.Append(rgbColorModelHex10);

            colorScheme1.Append(dark1Color1);
            colorScheme1.Append(light1Color1);
            colorScheme1.Append(dark2Color1);
            colorScheme1.Append(light2Color1);
            colorScheme1.Append(accent1Color1);
            colorScheme1.Append(accent2Color1);
            colorScheme1.Append(accent3Color1);
            colorScheme1.Append(accent4Color1);
            colorScheme1.Append(accent5Color1);
            colorScheme1.Append(accent6Color1);
            colorScheme1.Append(hyperlink1);
            colorScheme1.Append(followedHyperlinkColor1);

            A.FontScheme fontScheme4 = new A.FontScheme() { Name = "Office" };

            A.MajorFont majorFont1 = new A.MajorFont();
            A.LatinFont latinFont1 = new A.LatinFont() { Typeface = "Cambria", Panose = "020F0302020204030204" };
            A.EastAsianFont eastAsianFont1 = new A.EastAsianFont() { Typeface = "" };
            A.ComplexScriptFont complexScriptFont1 = new A.ComplexScriptFont() { Typeface = "" };
            A.SupplementalFont supplementalFont1 = new A.SupplementalFont() { Script = "Jpan", Typeface = "ＭＳ Ｐゴシック" };
            A.SupplementalFont supplementalFont2 = new A.SupplementalFont() { Script = "Hang", Typeface = "맑은 고딕" };
            A.SupplementalFont supplementalFont3 = new A.SupplementalFont() { Script = "Hans", Typeface = "宋体" };
            A.SupplementalFont supplementalFont4 = new A.SupplementalFont() { Script = "Hant", Typeface = "新細明體" };
            A.SupplementalFont supplementalFont5 = new A.SupplementalFont() { Script = "Arab", Typeface = "Times New Roman" };
            A.SupplementalFont supplementalFont6 = new A.SupplementalFont() { Script = "Hebr", Typeface = "Times New Roman" };
            A.SupplementalFont supplementalFont7 = new A.SupplementalFont() { Script = "Thai", Typeface = "Tahoma" };
            A.SupplementalFont supplementalFont8 = new A.SupplementalFont() { Script = "Ethi", Typeface = "Nyala" };
            A.SupplementalFont supplementalFont9 = new A.SupplementalFont() { Script = "Beng", Typeface = "Vrinda" };
            A.SupplementalFont supplementalFont10 = new A.SupplementalFont() { Script = "Gujr", Typeface = "Shruti" };
            A.SupplementalFont supplementalFont11 = new A.SupplementalFont() { Script = "Khmr", Typeface = "MoolBoran" };
            A.SupplementalFont supplementalFont12 = new A.SupplementalFont() { Script = "Knda", Typeface = "Tunga" };
            A.SupplementalFont supplementalFont13 = new A.SupplementalFont() { Script = "Guru", Typeface = "Raavi" };
            A.SupplementalFont supplementalFont14 = new A.SupplementalFont() { Script = "Cans", Typeface = "Euphemia" };
            A.SupplementalFont supplementalFont15 = new A.SupplementalFont() { Script = "Cher", Typeface = "Plantagenet Cherokee" };
            A.SupplementalFont supplementalFont16 = new A.SupplementalFont() { Script = "Yiii", Typeface = "Microsoft Yi Baiti" };
            A.SupplementalFont supplementalFont17 = new A.SupplementalFont() { Script = "Tibt", Typeface = "Microsoft Himalaya" };
            A.SupplementalFont supplementalFont18 = new A.SupplementalFont() { Script = "Thaa", Typeface = "MV Boli" };
            A.SupplementalFont supplementalFont19 = new A.SupplementalFont() { Script = "Deva", Typeface = "Mangal" };
            A.SupplementalFont supplementalFont20 = new A.SupplementalFont() { Script = "Telu", Typeface = "Gautami" };
            A.SupplementalFont supplementalFont21 = new A.SupplementalFont() { Script = "Taml", Typeface = "Latha" };
            A.SupplementalFont supplementalFont22 = new A.SupplementalFont() { Script = "Syrc", Typeface = "Estrangelo Edessa" };
            A.SupplementalFont supplementalFont23 = new A.SupplementalFont() { Script = "Orya", Typeface = "Kalinga" };
            A.SupplementalFont supplementalFont24 = new A.SupplementalFont() { Script = "Mlym", Typeface = "Kartika" };
            A.SupplementalFont supplementalFont25 = new A.SupplementalFont() { Script = "Laoo", Typeface = "DokChampa" };
            A.SupplementalFont supplementalFont26 = new A.SupplementalFont() { Script = "Sinh", Typeface = "Iskoola Pota" };
            A.SupplementalFont supplementalFont27 = new A.SupplementalFont() { Script = "Mong", Typeface = "Mongolian Baiti" };
            A.SupplementalFont supplementalFont28 = new A.SupplementalFont() { Script = "Viet", Typeface = "Times New Roman" };
            A.SupplementalFont supplementalFont29 = new A.SupplementalFont() { Script = "Uigh", Typeface = "Microsoft Uighur" };
            A.SupplementalFont supplementalFont30 = new A.SupplementalFont() { Script = "Geor", Typeface = "Sylfaen" };

            majorFont1.Append(latinFont1);
            majorFont1.Append(eastAsianFont1);
            majorFont1.Append(complexScriptFont1);
            majorFont1.Append(supplementalFont1);
            majorFont1.Append(supplementalFont2);
            majorFont1.Append(supplementalFont3);
            majorFont1.Append(supplementalFont4);
            majorFont1.Append(supplementalFont5);
            majorFont1.Append(supplementalFont6);
            majorFont1.Append(supplementalFont7);
            majorFont1.Append(supplementalFont8);
            majorFont1.Append(supplementalFont9);
            majorFont1.Append(supplementalFont10);
            majorFont1.Append(supplementalFont11);
            majorFont1.Append(supplementalFont12);
            majorFont1.Append(supplementalFont13);
            majorFont1.Append(supplementalFont14);
            majorFont1.Append(supplementalFont15);
            majorFont1.Append(supplementalFont16);
            majorFont1.Append(supplementalFont17);
            majorFont1.Append(supplementalFont18);
            majorFont1.Append(supplementalFont19);
            majorFont1.Append(supplementalFont20);
            majorFont1.Append(supplementalFont21);
            majorFont1.Append(supplementalFont22);
            majorFont1.Append(supplementalFont23);
            majorFont1.Append(supplementalFont24);
            majorFont1.Append(supplementalFont25);
            majorFont1.Append(supplementalFont26);
            majorFont1.Append(supplementalFont27);
            majorFont1.Append(supplementalFont28);
            majorFont1.Append(supplementalFont29);
            majorFont1.Append(supplementalFont30);

            A.MinorFont minorFont1 = new A.MinorFont();
            A.LatinFont latinFont2 = new A.LatinFont() { Typeface = "Calibri", Panose = "020F0502020204030204" };
            A.EastAsianFont eastAsianFont2 = new A.EastAsianFont() { Typeface = "" };
            A.ComplexScriptFont complexScriptFont2 = new A.ComplexScriptFont() { Typeface = "" };
            A.SupplementalFont supplementalFont31 = new A.SupplementalFont() { Script = "Jpan", Typeface = "ＭＳ Ｐゴシック" };
            A.SupplementalFont supplementalFont32 = new A.SupplementalFont() { Script = "Hang", Typeface = "맑은 고딕" };
            A.SupplementalFont supplementalFont33 = new A.SupplementalFont() { Script = "Hans", Typeface = "宋体" };
            A.SupplementalFont supplementalFont34 = new A.SupplementalFont() { Script = "Hant", Typeface = "新細明體" };
            A.SupplementalFont supplementalFont35 = new A.SupplementalFont() { Script = "Arab", Typeface = "Arial" };
            A.SupplementalFont supplementalFont36 = new A.SupplementalFont() { Script = "Hebr", Typeface = "Arial" };
            A.SupplementalFont supplementalFont37 = new A.SupplementalFont() { Script = "Thai", Typeface = "Tahoma" };
            A.SupplementalFont supplementalFont38 = new A.SupplementalFont() { Script = "Ethi", Typeface = "Nyala" };
            A.SupplementalFont supplementalFont39 = new A.SupplementalFont() { Script = "Beng", Typeface = "Vrinda" };
            A.SupplementalFont supplementalFont40 = new A.SupplementalFont() { Script = "Gujr", Typeface = "Shruti" };
            A.SupplementalFont supplementalFont41 = new A.SupplementalFont() { Script = "Khmr", Typeface = "DaunPenh" };
            A.SupplementalFont supplementalFont42 = new A.SupplementalFont() { Script = "Knda", Typeface = "Tunga" };
            A.SupplementalFont supplementalFont43 = new A.SupplementalFont() { Script = "Guru", Typeface = "Raavi" };
            A.SupplementalFont supplementalFont44 = new A.SupplementalFont() { Script = "Cans", Typeface = "Euphemia" };
            A.SupplementalFont supplementalFont45 = new A.SupplementalFont() { Script = "Cher", Typeface = "Plantagenet Cherokee" };
            A.SupplementalFont supplementalFont46 = new A.SupplementalFont() { Script = "Yiii", Typeface = "Microsoft Yi Baiti" };
            A.SupplementalFont supplementalFont47 = new A.SupplementalFont() { Script = "Tibt", Typeface = "Microsoft Himalaya" };
            A.SupplementalFont supplementalFont48 = new A.SupplementalFont() { Script = "Thaa", Typeface = "MV Boli" };
            A.SupplementalFont supplementalFont49 = new A.SupplementalFont() { Script = "Deva", Typeface = "Mangal" };
            A.SupplementalFont supplementalFont50 = new A.SupplementalFont() { Script = "Telu", Typeface = "Gautami" };
            A.SupplementalFont supplementalFont51 = new A.SupplementalFont() { Script = "Taml", Typeface = "Latha" };
            A.SupplementalFont supplementalFont52 = new A.SupplementalFont() { Script = "Syrc", Typeface = "Estrangelo Edessa" };
            A.SupplementalFont supplementalFont53 = new A.SupplementalFont() { Script = "Orya", Typeface = "Kalinga" };
            A.SupplementalFont supplementalFont54 = new A.SupplementalFont() { Script = "Mlym", Typeface = "Kartika" };
            A.SupplementalFont supplementalFont55 = new A.SupplementalFont() { Script = "Laoo", Typeface = "DokChampa" };
            A.SupplementalFont supplementalFont56 = new A.SupplementalFont() { Script = "Sinh", Typeface = "Iskoola Pota" };
            A.SupplementalFont supplementalFont57 = new A.SupplementalFont() { Script = "Mong", Typeface = "Mongolian Baiti" };
            A.SupplementalFont supplementalFont58 = new A.SupplementalFont() { Script = "Viet", Typeface = "Arial" };
            A.SupplementalFont supplementalFont59 = new A.SupplementalFont() { Script = "Uigh", Typeface = "Microsoft Uighur" };
            A.SupplementalFont supplementalFont60 = new A.SupplementalFont() { Script = "Geor", Typeface = "Sylfaen" };

            minorFont1.Append(latinFont2);
            minorFont1.Append(eastAsianFont2);
            minorFont1.Append(complexScriptFont2);
            minorFont1.Append(supplementalFont31);
            minorFont1.Append(supplementalFont32);
            minorFont1.Append(supplementalFont33);
            minorFont1.Append(supplementalFont34);
            minorFont1.Append(supplementalFont35);
            minorFont1.Append(supplementalFont36);
            minorFont1.Append(supplementalFont37);
            minorFont1.Append(supplementalFont38);
            minorFont1.Append(supplementalFont39);
            minorFont1.Append(supplementalFont40);
            minorFont1.Append(supplementalFont41);
            minorFont1.Append(supplementalFont42);
            minorFont1.Append(supplementalFont43);
            minorFont1.Append(supplementalFont44);
            minorFont1.Append(supplementalFont45);
            minorFont1.Append(supplementalFont46);
            minorFont1.Append(supplementalFont47);
            minorFont1.Append(supplementalFont48);
            minorFont1.Append(supplementalFont49);
            minorFont1.Append(supplementalFont50);
            minorFont1.Append(supplementalFont51);
            minorFont1.Append(supplementalFont52);
            minorFont1.Append(supplementalFont53);
            minorFont1.Append(supplementalFont54);
            minorFont1.Append(supplementalFont55);
            minorFont1.Append(supplementalFont56);
            minorFont1.Append(supplementalFont57);
            minorFont1.Append(supplementalFont58);
            minorFont1.Append(supplementalFont59);
            minorFont1.Append(supplementalFont60);

            fontScheme4.Append(majorFont1);
            fontScheme4.Append(minorFont1);

            A.FormatScheme formatScheme1 = new A.FormatScheme() { Name = "Office" };

            A.FillStyleList fillStyleList1 = new A.FillStyleList();

            A.SolidFill solidFill1 = new A.SolidFill();
            A.SchemeColor schemeColor1 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

            solidFill1.Append(schemeColor1);

            A.GradientFill gradientFill1 = new A.GradientFill() { RotateWithShape = true };

            A.GradientStopList gradientStopList1 = new A.GradientStopList();

            A.GradientStop gradientStop1 = new A.GradientStop() { Position = 0 };

            A.SchemeColor schemeColor2 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Tint tint1 = new A.Tint() { Val = 50000 };
            A.SaturationModulation saturationModulation1 = new A.SaturationModulation() { Val = 300000 };

            schemeColor2.Append(tint1);
            schemeColor2.Append(saturationModulation1);

            gradientStop1.Append(schemeColor2);

            A.GradientStop gradientStop2 = new A.GradientStop() { Position = 35000 };

            A.SchemeColor schemeColor3 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Tint tint2 = new A.Tint() { Val = 37000 };
            A.SaturationModulation saturationModulation2 = new A.SaturationModulation() { Val = 300000 };

            schemeColor3.Append(tint2);
            schemeColor3.Append(saturationModulation2);

            gradientStop2.Append(schemeColor3);

            A.GradientStop gradientStop3 = new A.GradientStop() { Position = 100000 };

            A.SchemeColor schemeColor4 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Tint tint3 = new A.Tint() { Val = 15000 };
            A.SaturationModulation saturationModulation3 = new A.SaturationModulation() { Val = 350000 };

            schemeColor4.Append(tint3);
            schemeColor4.Append(saturationModulation3);

            gradientStop3.Append(schemeColor4);

            gradientStopList1.Append(gradientStop1);
            gradientStopList1.Append(gradientStop2);
            gradientStopList1.Append(gradientStop3);
            A.LinearGradientFill linearGradientFill1 = new A.LinearGradientFill() { Angle = 16200000, Scaled = true };

            gradientFill1.Append(gradientStopList1);
            gradientFill1.Append(linearGradientFill1);

            A.GradientFill gradientFill2 = new A.GradientFill() { RotateWithShape = true };

            A.GradientStopList gradientStopList2 = new A.GradientStopList();

            A.GradientStop gradientStop4 = new A.GradientStop() { Position = 0 };

            A.SchemeColor schemeColor5 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Shade shade1 = new A.Shade() { Val = 51000 };
            A.SaturationModulation saturationModulation4 = new A.SaturationModulation() { Val = 130000 };

            schemeColor5.Append(shade1);
            schemeColor5.Append(saturationModulation4);

            gradientStop4.Append(schemeColor5);

            A.GradientStop gradientStop5 = new A.GradientStop() { Position = 80000 };

            A.SchemeColor schemeColor6 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Shade shade2 = new A.Shade() { Val = 93000 };
            A.SaturationModulation saturationModulation5 = new A.SaturationModulation() { Val = 130000 };

            schemeColor6.Append(shade2);
            schemeColor6.Append(saturationModulation5);

            gradientStop5.Append(schemeColor6);

            A.GradientStop gradientStop6 = new A.GradientStop() { Position = 100000 };

            A.SchemeColor schemeColor7 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Shade shade3 = new A.Shade() { Val = 94000 };
            A.SaturationModulation saturationModulation6 = new A.SaturationModulation() { Val = 135000 };

            schemeColor7.Append(shade3);
            schemeColor7.Append(saturationModulation6);

            gradientStop6.Append(schemeColor7);

            gradientStopList2.Append(gradientStop4);
            gradientStopList2.Append(gradientStop5);
            gradientStopList2.Append(gradientStop6);
            A.LinearGradientFill linearGradientFill2 = new A.LinearGradientFill() { Angle = 16200000, Scaled = false };

            gradientFill2.Append(gradientStopList2);
            gradientFill2.Append(linearGradientFill2);

            fillStyleList1.Append(solidFill1);
            fillStyleList1.Append(gradientFill1);
            fillStyleList1.Append(gradientFill2);

            A.LineStyleList lineStyleList1 = new A.LineStyleList();

            A.Outline outline1 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill2 = new A.SolidFill();

            A.SchemeColor schemeColor8 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Shade shade4 = new A.Shade() { Val = 95000 };
            A.SaturationModulation saturationModulation7 = new A.SaturationModulation() { Val = 105000 };

            schemeColor8.Append(shade4);
            schemeColor8.Append(saturationModulation7);

            solidFill2.Append(schemeColor8);
            A.PresetDash presetDash1 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

            outline1.Append(solidFill2);
            outline1.Append(presetDash1);

            A.Outline outline2 = new A.Outline() { Width = 25400, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill3 = new A.SolidFill();
            A.SchemeColor schemeColor9 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

            solidFill3.Append(schemeColor9);
            A.PresetDash presetDash2 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

            outline2.Append(solidFill3);
            outline2.Append(presetDash2);

            A.Outline outline3 = new A.Outline() { Width = 38100, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill4 = new A.SolidFill();
            A.SchemeColor schemeColor10 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

            solidFill4.Append(schemeColor10);
            A.PresetDash presetDash3 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

            outline3.Append(solidFill4);
            outline3.Append(presetDash3);

            lineStyleList1.Append(outline1);
            lineStyleList1.Append(outline2);
            lineStyleList1.Append(outline3);

            A.EffectStyleList effectStyleList1 = new A.EffectStyleList();

            A.EffectStyle effectStyle1 = new A.EffectStyle();

            A.EffectList effectList1 = new A.EffectList();

            A.OuterShadow outerShadow1 = new A.OuterShadow() { BlurRadius = 40000L, Distance = 20000L, Direction = 5400000, RotateWithShape = false };

            A.RgbColorModelHex rgbColorModelHex11 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha1 = new A.Alpha() { Val = 38000 };

            rgbColorModelHex11.Append(alpha1);

            outerShadow1.Append(rgbColorModelHex11);

            effectList1.Append(outerShadow1);

            effectStyle1.Append(effectList1);

            A.EffectStyle effectStyle2 = new A.EffectStyle();

            A.EffectList effectList2 = new A.EffectList();

            A.OuterShadow outerShadow2 = new A.OuterShadow() { BlurRadius = 40000L, Distance = 23000L, Direction = 5400000, RotateWithShape = false };

            A.RgbColorModelHex rgbColorModelHex12 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha2 = new A.Alpha() { Val = 35000 };

            rgbColorModelHex12.Append(alpha2);

            outerShadow2.Append(rgbColorModelHex12);

            effectList2.Append(outerShadow2);

            effectStyle2.Append(effectList2);

            A.EffectStyle effectStyle3 = new A.EffectStyle();

            A.EffectList effectList3 = new A.EffectList();

            A.OuterShadow outerShadow3 = new A.OuterShadow() { BlurRadius = 40000L, Distance = 23000L, Direction = 5400000, RotateWithShape = false };

            A.RgbColorModelHex rgbColorModelHex13 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha3 = new A.Alpha() { Val = 35000 };

            rgbColorModelHex13.Append(alpha3);

            outerShadow3.Append(rgbColorModelHex13);

            effectList3.Append(outerShadow3);

            A.Scene3DType scene3DType1 = new A.Scene3DType();

            A.Camera camera1 = new A.Camera() { Preset = A.PresetCameraValues.OrthographicFront };
            A.Rotation rotation1 = new A.Rotation() { Latitude = 0, Longitude = 0, Revolution = 0 };

            camera1.Append(rotation1);

            A.LightRig lightRig1 = new A.LightRig() { Rig = A.LightRigValues.ThreePoints, Direction = A.LightRigDirectionValues.Top };
            A.Rotation rotation2 = new A.Rotation() { Latitude = 0, Longitude = 0, Revolution = 1200000 };

            lightRig1.Append(rotation2);

            scene3DType1.Append(camera1);
            scene3DType1.Append(lightRig1);

            A.Shape3DType shape3DType1 = new A.Shape3DType();
            A.BevelTop bevelTop1 = new A.BevelTop() { Width = 63500L, Height = 25400L };

            shape3DType1.Append(bevelTop1);

            effectStyle3.Append(effectList3);
            effectStyle3.Append(scene3DType1);
            effectStyle3.Append(shape3DType1);

            effectStyleList1.Append(effectStyle1);
            effectStyleList1.Append(effectStyle2);
            effectStyleList1.Append(effectStyle3);

            A.BackgroundFillStyleList backgroundFillStyleList1 = new A.BackgroundFillStyleList();

            A.SolidFill solidFill5 = new A.SolidFill();
            A.SchemeColor schemeColor11 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

            solidFill5.Append(schemeColor11);

            A.GradientFill gradientFill3 = new A.GradientFill() { RotateWithShape = true };

            A.GradientStopList gradientStopList3 = new A.GradientStopList();

            A.GradientStop gradientStop7 = new A.GradientStop() { Position = 0 };

            A.SchemeColor schemeColor12 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Tint tint4 = new A.Tint() { Val = 40000 };
            A.SaturationModulation saturationModulation8 = new A.SaturationModulation() { Val = 350000 };

            schemeColor12.Append(tint4);
            schemeColor12.Append(saturationModulation8);

            gradientStop7.Append(schemeColor12);

            A.GradientStop gradientStop8 = new A.GradientStop() { Position = 40000 };

            A.SchemeColor schemeColor13 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Tint tint5 = new A.Tint() { Val = 45000 };
            A.Shade shade5 = new A.Shade() { Val = 99000 };
            A.SaturationModulation saturationModulation9 = new A.SaturationModulation() { Val = 350000 };

            schemeColor13.Append(tint5);
            schemeColor13.Append(shade5);
            schemeColor13.Append(saturationModulation9);

            gradientStop8.Append(schemeColor13);

            A.GradientStop gradientStop9 = new A.GradientStop() { Position = 100000 };

            A.SchemeColor schemeColor14 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Shade shade6 = new A.Shade() { Val = 20000 };
            A.SaturationModulation saturationModulation10 = new A.SaturationModulation() { Val = 255000 };

            schemeColor14.Append(shade6);
            schemeColor14.Append(saturationModulation10);

            gradientStop9.Append(schemeColor14);

            gradientStopList3.Append(gradientStop7);
            gradientStopList3.Append(gradientStop8);
            gradientStopList3.Append(gradientStop9);

            A.PathGradientFill pathGradientFill1 = new A.PathGradientFill() { Path = A.PathShadeValues.Circle };
            A.FillToRectangle fillToRectangle1 = new A.FillToRectangle() { Left = 50000, Top = -80000, Right = 50000, Bottom = 180000 };

            pathGradientFill1.Append(fillToRectangle1);

            gradientFill3.Append(gradientStopList3);
            gradientFill3.Append(pathGradientFill1);

            A.GradientFill gradientFill4 = new A.GradientFill() { RotateWithShape = true };

            A.GradientStopList gradientStopList4 = new A.GradientStopList();

            A.GradientStop gradientStop10 = new A.GradientStop() { Position = 0 };

            A.SchemeColor schemeColor15 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Tint tint6 = new A.Tint() { Val = 80000 };
            A.SaturationModulation saturationModulation11 = new A.SaturationModulation() { Val = 300000 };

            schemeColor15.Append(tint6);
            schemeColor15.Append(saturationModulation11);

            gradientStop10.Append(schemeColor15);

            A.GradientStop gradientStop11 = new A.GradientStop() { Position = 100000 };

            A.SchemeColor schemeColor16 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Shade shade7 = new A.Shade() { Val = 30000 };
            A.SaturationModulation saturationModulation12 = new A.SaturationModulation() { Val = 200000 };

            schemeColor16.Append(shade7);
            schemeColor16.Append(saturationModulation12);

            gradientStop11.Append(schemeColor16);

            gradientStopList4.Append(gradientStop10);
            gradientStopList4.Append(gradientStop11);

            A.PathGradientFill pathGradientFill2 = new A.PathGradientFill() { Path = A.PathShadeValues.Circle };
            A.FillToRectangle fillToRectangle2 = new A.FillToRectangle() { Left = 50000, Top = 50000, Right = 50000, Bottom = 50000 };

            pathGradientFill2.Append(fillToRectangle2);

            gradientFill4.Append(gradientStopList4);
            gradientFill4.Append(pathGradientFill2);

            backgroundFillStyleList1.Append(solidFill5);
            backgroundFillStyleList1.Append(gradientFill3);
            backgroundFillStyleList1.Append(gradientFill4);

            formatScheme1.Append(fillStyleList1);
            formatScheme1.Append(lineStyleList1);
            formatScheme1.Append(effectStyleList1);
            formatScheme1.Append(backgroundFillStyleList1);

            themeElements1.Append(colorScheme1);
            themeElements1.Append(fontScheme4);
            themeElements1.Append(formatScheme1);
            A.ObjectDefaults objectDefaults1 = new A.ObjectDefaults();
            A.ExtraColorSchemeList extraColorSchemeList1 = new A.ExtraColorSchemeList();

            theme1.Append(themeElements1);
            theme1.Append(objectDefaults1);
            theme1.Append(extraColorSchemeList1);

            themePart1.Theme = theme1;
        }

        // Generates content of worksheetPart1.
        private void GenerateWorksheetPart1Content(WorksheetPart worksheetPart1)
        {
            Worksheet worksheet1 = new Worksheet() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "x14ac" } };
            worksheet1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            worksheet1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            worksheet1.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");
            SheetDimension sheetDimension1 = new SheetDimension() { Reference = "A1:N11" };

            SheetViews sheetViews1 = new SheetViews();

            SheetView sheetView1 = new SheetView() { TabSelected = true, WorkbookViewId = (UInt32Value)0U };
            Selection selection1 = new Selection() { ActiveCell = "G6", SequenceOfReferences = new ListValue<StringValue>() { InnerText = "G6" } };

            sheetView1.Append(selection1);

            sheetViews1.Append(sheetView1);
            SheetFormatProperties sheetFormatProperties1 = new SheetFormatProperties() { DefaultRowHeight = 15D, DyDescent = 0.25D };

            Columns columns1 = new Columns();
            Column column1 = new Column() { Min = (UInt32Value)1U, Max = (UInt32Value)1U, Width = 13.7109375D, CustomWidth = true };
            Column column2 = new Column() { Min = (UInt32Value)2U, Max = (UInt32Value)2U, Width = 25.85546875D, CustomWidth = true };
            Column column3 = new Column() { Min = (UInt32Value)3U, Max = (UInt32Value)3U, Width = 20.7109375D, CustomWidth = true };
            Column column4 = new Column() { Min = (UInt32Value)4U, Max = (UInt32Value)4U, Width = 18.42578125D, CustomWidth = true };
            Column column5 = new Column() { Min = (UInt32Value)5U, Max = (UInt32Value)5U, Width = 12.7109375D, Style = (UInt32Value)1U, CustomWidth = true };
            Column column6 = new Column() { Min = (UInt32Value)6U, Max = (UInt32Value)6U, Width = 8D, CustomWidth = true };
            Column column7 = new Column() { Min = (UInt32Value)7U, Max = (UInt32Value)7U, Width = 7.5703125D, CustomWidth = true };
            Column column8 = new Column() { Min = (UInt32Value)8U, Max = (UInt32Value)8U, Width = 7.85546875D, CustomWidth = true };
            Column column9 = new Column() { Min = (UInt32Value)9U, Max = (UInt32Value)9U, Width = 8.140625D, CustomWidth = true };
            Column column10 = new Column() { Min = (UInt32Value)10U, Max = (UInt32Value)10U, Width = 7.7109375D, CustomWidth = true };
            Column column11 = new Column() { Min = (UInt32Value)14U, Max = (UInt32Value)14U, Width = 12.7109375D, CustomWidth = true };

            columns1.Append(column1);
            columns1.Append(column2);
            columns1.Append(column3);
            columns1.Append(column4);
            columns1.Append(column5);
            columns1.Append(column6);
            columns1.Append(column7);
            columns1.Append(column8);
            columns1.Append(column9);
            columns1.Append(column10);
            columns1.Append(column11);

            SheetData sheetData1 = new SheetData();

            Row row1 = new Row() { RowIndex = (UInt32Value)1U, Spans = new ListValue<StringValue>() { InnerText = "1:14" }, StyleIndex = (UInt32Value)2U, CustomFormat = true, Height = 89.25D, CustomHeight = true, DyDescent = 0.25D };

            Cell cell1 = new Cell() { CellReference = "A1", StyleIndex = (UInt32Value)24U, DataType = CellValues.SharedString };
            CellValue cellValue1 = new CellValue();
            cellValue1.Text = "16";

            cell1.Append(cellValue1);
            Cell cell2 = new Cell() { CellReference = "B1", StyleIndex = (UInt32Value)24U };
            Cell cell3 = new Cell() { CellReference = "C1", StyleIndex = (UInt32Value)24U };
            Cell cell4 = new Cell() { CellReference = "D1", StyleIndex = (UInt32Value)24U };
            Cell cell5 = new Cell() { CellReference = "E1", StyleIndex = (UInt32Value)24U };
            Cell cell6 = new Cell() { CellReference = "F1", StyleIndex = (UInt32Value)24U };
            Cell cell7 = new Cell() { CellReference = "G1", StyleIndex = (UInt32Value)24U };
            Cell cell8 = new Cell() { CellReference = "H1", StyleIndex = (UInt32Value)24U };
            Cell cell9 = new Cell() { CellReference = "I1", StyleIndex = (UInt32Value)24U };
            Cell cell10 = new Cell() { CellReference = "J1", StyleIndex = (UInt32Value)24U };

            row1.Append(cell1);
            row1.Append(cell2);
            row1.Append(cell3);
            row1.Append(cell4);
            row1.Append(cell5);
            row1.Append(cell6);
            row1.Append(cell7);
            row1.Append(cell8);
            row1.Append(cell9);
            row1.Append(cell10);

            Row row2 = new Row() { RowIndex = (UInt32Value)2U, Spans = new ListValue<StringValue>() { InnerText = "1:14" }, StyleIndex = (UInt32Value)2U, CustomFormat = true, Height = 22.5D, CustomHeight = true, DyDescent = 0.25D };

            Cell cell11 = new Cell() { CellReference = "A2", StyleIndex = (UInt32Value)5U, DataType = CellValues.SharedString };
            CellValue cellValue2 = new CellValue();
            cellValue2.Text = "13";

            cell11.Append(cellValue2);

            Cell cell12 = new Cell() { CellReference = "B2", StyleIndex = (UInt32Value)25U, DataType = CellValues.SharedString };
            CellValue cellValue3 = new CellValue();
            cellValue3.Text = "17";

            cell12.Append(cellValue3);
            Cell cell13 = new Cell() { CellReference = "C2", StyleIndex = (UInt32Value)26U };
            Cell cell14 = new Cell() { CellReference = "D2", StyleIndex = (UInt32Value)4U };
            Cell cell15 = new Cell() { CellReference = "E2", StyleIndex = (UInt32Value)4U };
            Cell cell16 = new Cell() { CellReference = "F2", StyleIndex = (UInt32Value)4U };
            Cell cell17 = new Cell() { CellReference = "G2", StyleIndex = (UInt32Value)4U };
            Cell cell18 = new Cell() { CellReference = "H2", StyleIndex = (UInt32Value)4U };
            Cell cell19 = new Cell() { CellReference = "I2", StyleIndex = (UInt32Value)4U };
            Cell cell20 = new Cell() { CellReference = "J2", StyleIndex = (UInt32Value)4U };

            row2.Append(cell11);
            row2.Append(cell12);
            row2.Append(cell13);
            row2.Append(cell14);
            row2.Append(cell15);
            row2.Append(cell16);
            row2.Append(cell17);
            row2.Append(cell18);
            row2.Append(cell19);
            row2.Append(cell20);

            Row row3 = new Row() { RowIndex = (UInt32Value)3U, Spans = new ListValue<StringValue>() { InnerText = "1:14" }, StyleIndex = (UInt32Value)2U, CustomFormat = true, Height = 22.5D, CustomHeight = true, DyDescent = 0.25D };

            Cell cell21 = new Cell() { CellReference = "A3", StyleIndex = (UInt32Value)6U, DataType = CellValues.SharedString };
            CellValue cellValue4 = new CellValue();
            cellValue4.Text = "14";

            cell21.Append(cellValue4);

            Cell cell22 = new Cell() { CellReference = "B3", StyleIndex = (UInt32Value)27U, DataType = CellValues.SharedString };
            CellValue cellValue5 = new CellValue();
            cellValue5.Text = "17";

            cell22.Append(cellValue5);
            Cell cell23 = new Cell() { CellReference = "C3", StyleIndex = (UInt32Value)28U };
            Cell cell24 = new Cell() { CellReference = "D3", StyleIndex = (UInt32Value)4U };
            Cell cell25 = new Cell() { CellReference = "E3", StyleIndex = (UInt32Value)4U };
            Cell cell26 = new Cell() { CellReference = "F3", StyleIndex = (UInt32Value)4U };
            Cell cell27 = new Cell() { CellReference = "G3", StyleIndex = (UInt32Value)4U };
            Cell cell28 = new Cell() { CellReference = "H3", StyleIndex = (UInt32Value)7U };
            Cell cell29 = new Cell() { CellReference = "I3", StyleIndex = (UInt32Value)8U };
            Cell cell30 = new Cell() { CellReference = "J3", StyleIndex = (UInt32Value)8U };

            Cell cell31 = new Cell() { CellReference = "K3", StyleIndex = (UInt32Value)9U, DataType = CellValues.SharedString };
            CellValue cellValue6 = new CellValue();
            cellValue6.Text = "15";

            cell31.Append(cellValue6);
            Cell cell32 = new Cell() { CellReference = "L3", StyleIndex = (UInt32Value)23U };
            Cell cell33 = new Cell() { CellReference = "M3", StyleIndex = (UInt32Value)23U };
            Cell cell34 = new Cell() { CellReference = "N3", StyleIndex = (UInt32Value)23U };

            row3.Append(cell21);
            row3.Append(cell22);
            row3.Append(cell23);
            row3.Append(cell24);
            row3.Append(cell25);
            row3.Append(cell26);
            row3.Append(cell27);
            row3.Append(cell28);
            row3.Append(cell29);
            row3.Append(cell30);
            row3.Append(cell31);
            row3.Append(cell32);
            row3.Append(cell33);
            row3.Append(cell34);

            Row row4 = new Row() { RowIndex = (UInt32Value)4U, Spans = new ListValue<StringValue>() { InnerText = "1:14" }, Height = 30.75D, CustomHeight = true, DyDescent = 0.25D };

            Cell cell35 = new Cell() { CellReference = "A4", StyleIndex = (UInt32Value)29U, DataType = CellValues.SharedString };
            CellValue cellValue7 = new CellValue();
            cellValue7.Text = "12";

            cell35.Append(cellValue7);
            Cell cell36 = new Cell() { CellReference = "B4", StyleIndex = (UInt32Value)29U };
            Cell cell37 = new Cell() { CellReference = "C4", StyleIndex = (UInt32Value)29U };
            Cell cell38 = new Cell() { CellReference = "D4", StyleIndex = (UInt32Value)29U };
            Cell cell39 = new Cell() { CellReference = "E4", StyleIndex = (UInt32Value)29U };
            Cell cell40 = new Cell() { CellReference = "F4", StyleIndex = (UInt32Value)29U };
            Cell cell41 = new Cell() { CellReference = "G4", StyleIndex = (UInt32Value)29U };
            Cell cell42 = new Cell() { CellReference = "H4", StyleIndex = (UInt32Value)29U };
            Cell cell43 = new Cell() { CellReference = "I4", StyleIndex = (UInt32Value)29U };
            Cell cell44 = new Cell() { CellReference = "J4", StyleIndex = (UInt32Value)29U };
            Cell cell45 = new Cell() { CellReference = "K4", StyleIndex = (UInt32Value)29U };
            Cell cell46 = new Cell() { CellReference = "L4", StyleIndex = (UInt32Value)29U };
            Cell cell47 = new Cell() { CellReference = "M4", StyleIndex = (UInt32Value)29U };
            Cell cell48 = new Cell() { CellReference = "N4", StyleIndex = (UInt32Value)29U };

            row4.Append(cell35);
            row4.Append(cell36);
            row4.Append(cell37);
            row4.Append(cell38);
            row4.Append(cell39);
            row4.Append(cell40);
            row4.Append(cell41);
            row4.Append(cell42);
            row4.Append(cell43);
            row4.Append(cell44);
            row4.Append(cell45);
            row4.Append(cell46);
            row4.Append(cell47);
            row4.Append(cell48);

            Row row5 = new Row() { RowIndex = (UInt32Value)5U, Spans = new ListValue<StringValue>() { InnerText = "1:14" }, Height = 29.25D, CustomHeight = true, DyDescent = 0.25D };

            Cell cell49 = new Cell() { CellReference = "A5", StyleIndex = (UInt32Value)30U, DataType = CellValues.SharedString };
            CellValue cellValue8 = new CellValue();
            cellValue8.Text = "0";

            cell49.Append(cellValue8);

            Cell cell50 = new Cell() { CellReference = "B5", StyleIndex = (UInt32Value)30U, DataType = CellValues.SharedString };
            CellValue cellValue9 = new CellValue();
            cellValue9.Text = "1";

            cell50.Append(cellValue9);

            Cell cell51 = new Cell() { CellReference = "C5", StyleIndex = (UInt32Value)36U, DataType = CellValues.SharedString };
            CellValue cellValue10 = new CellValue();
            cellValue10.Text = "2";

            cell51.Append(cellValue10);

            Cell cell52 = new Cell() { CellReference = "D5", StyleIndex = (UInt32Value)30U, DataType = CellValues.SharedString };
            CellValue cellValue11 = new CellValue();
            cellValue11.Text = "3";

            cell52.Append(cellValue11);

            Cell cell53 = new Cell() { CellReference = "E5", StyleIndex = (UInt32Value)36U, DataType = CellValues.SharedString };
            CellValue cellValue12 = new CellValue();
            cellValue12.Text = "10";

            cell53.Append(cellValue12);

            Cell cell54 = new Cell() { CellReference = "F5", StyleIndex = (UInt32Value)31U, DataType = CellValues.SharedString };
            CellValue cellValue13 = new CellValue();
            cellValue13.Text = "4";

            cell54.Append(cellValue13);
            Cell cell55 = new Cell() { CellReference = "G5", StyleIndex = (UInt32Value)32U };

            Cell cell56 = new Cell() { CellReference = "H5", StyleIndex = (UInt32Value)33U, DataType = CellValues.SharedString };
            CellValue cellValue14 = new CellValue();
            cellValue14.Text = "11";

            cell56.Append(cellValue14);
            Cell cell57 = new Cell() { CellReference = "I5", StyleIndex = (UInt32Value)34U };
            Cell cell58 = new Cell() { CellReference = "J5", StyleIndex = (UInt32Value)35U };

            Cell cell59 = new Cell() { CellReference = "K5", StyleIndex = (UInt32Value)19U, DataType = CellValues.SharedString };
            CellValue cellValue15 = new CellValue();
            cellValue15.Text = "19";

            cell59.Append(cellValue15);

            Cell cell60 = new Cell() { CellReference = "L5", StyleIndex = (UInt32Value)19U, DataType = CellValues.SharedString };
            CellValue cellValue16 = new CellValue();
            cellValue16.Text = "20";

            cell60.Append(cellValue16);

            Cell cell61 = new Cell() { CellReference = "M5", StyleIndex = (UInt32Value)19U, DataType = CellValues.SharedString };
            CellValue cellValue17 = new CellValue();
            cellValue17.Text = "21";

            cell61.Append(cellValue17);

            Cell cell62 = new Cell() { CellReference = "N5", StyleIndex = (UInt32Value)21U, DataType = CellValues.SharedString };
            CellValue cellValue18 = new CellValue();
            cellValue18.Text = "22";

            cell62.Append(cellValue18);

            row5.Append(cell49);
            row5.Append(cell50);
            row5.Append(cell51);
            row5.Append(cell52);
            row5.Append(cell53);
            row5.Append(cell54);
            row5.Append(cell55);
            row5.Append(cell56);
            row5.Append(cell57);
            row5.Append(cell58);
            row5.Append(cell59);
            row5.Append(cell60);
            row5.Append(cell61);
            row5.Append(cell62);

            Row row6 = new Row() { RowIndex = (UInt32Value)6U, Spans = new ListValue<StringValue>() { InnerText = "1:14" }, Height = 29.25D, CustomHeight = true, DyDescent = 0.25D };
            Cell cell63 = new Cell() { CellReference = "A6", StyleIndex = (UInt32Value)30U };
            Cell cell64 = new Cell() { CellReference = "B6", StyleIndex = (UInt32Value)30U };
            Cell cell65 = new Cell() { CellReference = "C6", StyleIndex = (UInt32Value)37U };
            Cell cell66 = new Cell() { CellReference = "D6", StyleIndex = (UInt32Value)30U };
            Cell cell67 = new Cell() { CellReference = "E6", StyleIndex = (UInt32Value)37U };

            Cell cell68 = new Cell() { CellReference = "F6", StyleIndex = (UInt32Value)10U, DataType = CellValues.SharedString };
            CellValue cellValue19 = new CellValue();
            cellValue19.Text = "5";

            cell68.Append(cellValue19);

            Cell cell69 = new Cell() { CellReference = "G6", StyleIndex = (UInt32Value)10U, DataType = CellValues.SharedString };
            CellValue cellValue20 = new CellValue();
            cellValue20.Text = "6";

            cell69.Append(cellValue20);

            Cell cell70 = new Cell() { CellReference = "H6", StyleIndex = (UInt32Value)11U, DataType = CellValues.SharedString };
            CellValue cellValue21 = new CellValue();
            cellValue21.Text = "7";

            cell70.Append(cellValue21);

            Cell cell71 = new Cell() { CellReference = "I6", StyleIndex = (UInt32Value)11U, DataType = CellValues.SharedString };
            CellValue cellValue22 = new CellValue();
            cellValue22.Text = "8";

            cell71.Append(cellValue22);

            Cell cell72 = new Cell() { CellReference = "J6", StyleIndex = (UInt32Value)11U, DataType = CellValues.SharedString };
            CellValue cellValue23 = new CellValue();
            cellValue23.Text = "9";

            cell72.Append(cellValue23);
            Cell cell73 = new Cell() { CellReference = "K6", StyleIndex = (UInt32Value)20U };
            Cell cell74 = new Cell() { CellReference = "L6", StyleIndex = (UInt32Value)20U };
            Cell cell75 = new Cell() { CellReference = "M6", StyleIndex = (UInt32Value)20U };
            Cell cell76 = new Cell() { CellReference = "N6", StyleIndex = (UInt32Value)22U };

            row6.Append(cell63);
            row6.Append(cell64);
            row6.Append(cell65);
            row6.Append(cell66);
            row6.Append(cell67);
            row6.Append(cell68);
            row6.Append(cell69);
            row6.Append(cell70);
            row6.Append(cell71);
            row6.Append(cell72);
            row6.Append(cell73);
            row6.Append(cell74);
            row6.Append(cell75);
            row6.Append(cell76);

            

            //CUSTOM FOOTER
            var footerShift = 7 + _quotationModel.QuotationItems.Count;


            //Row row7 = new Row()
            //{
            //    RowIndex = Convert.ToUInt32(footerShift),
            //    Spans = new ListValue<StringValue>() { InnerText = "1:14" },
            //    Height = 86.25D,
            //    CustomHeight = true,
            //    DyDescent = 0.25D
            //};
            //Cell cell77 = new Cell() { CellReference = "A" + Convert.ToUInt32(footerShift), StyleIndex = (UInt32Value)10U };
            //Cell cell78 = new Cell() { CellReference = "B" + Convert.ToUInt32(footerShift), StyleIndex = (UInt32Value)12U };
            //Cell cell79 = new Cell() { CellReference = "C" + Convert.ToUInt32(footerShift), StyleIndex = (UInt32Value)13U };
            //Cell cell80 = new Cell() { CellReference = "D" + Convert.ToUInt32(footerShift), StyleIndex = (UInt32Value)12U };

            //Cell cell81 = new Cell() { CellReference = "E" + Convert.ToUInt32(footerShift), StyleIndex = (UInt32Value)15U, DataType = CellValues.SharedString };
            //CellValue cellValue24 = new CellValue();
            //cellValue24.Text = "24";
            //cell81.Append(cellValue24);

            //Cell cell82 = new Cell() { CellReference = "F" + Convert.ToUInt32(footerShift), StyleIndex = (UInt32Value)16U, DataType = CellValues.SharedString };
            //CellValue cellValue25 = new CellValue();
            //cellValue25.Text = "25";
            //cell82.Append(cellValue25);

            //Cell cell83 = new Cell() { CellReference = "G" + Convert.ToUInt32(footerShift), StyleIndex = (UInt32Value)16U, DataType = CellValues.SharedString };
            //CellValue cellValue26 = new CellValue();
            //cellValue26.Text = "26";
            //cell83.Append(cellValue26);

            //Cell cell84 = new Cell() { CellReference = "H" + Convert.ToUInt32(footerShift), StyleIndex = (UInt32Value)17U, DataType = CellValues.SharedString };
            //CellValue cellValue27 = new CellValue();
            //cellValue27.Text = "8";
            //cell84.Append(cellValue27);

            //Cell cell85 = new Cell() { CellReference = "I" + Convert.ToUInt32(footerShift), StyleIndex = (UInt32Value)17U, DataType = CellValues.SharedString };
            //CellValue cellValue28 = new CellValue();
            //cellValue28.Text = "27";
            //cell85.Append(cellValue28);

            //Cell cell86 = new Cell() { CellReference = "J" + Convert.ToUInt32(footerShift), StyleIndex = (UInt32Value)17U, DataType = CellValues.SharedString };
            //CellValue cellValue29 = new CellValue();
            //cellValue29.Text = "28";
            //cell86.Append(cellValue29);

            //Cell cell87 = new Cell() { CellReference = "K" + Convert.ToUInt32(footerShift), StyleIndex = (UInt32Value)18U, DataType = CellValues.SharedString };
            //CellValue cellValue30 = new CellValue();
            //cellValue30.Text = "29";
            //cell87.Append(cellValue30);

            //Cell cell88 = new Cell() { CellReference = "L" + Convert.ToUInt32(footerShift), StyleIndex = (UInt32Value)18U, DataType = CellValues.SharedString };
            //CellValue cellValue31 = new CellValue();
            //cellValue31.Text = "30";
            //cell88.Append(cellValue31);

            //Cell cell89 = new Cell() { CellReference = "M" + Convert.ToUInt32(footerShift), StyleIndex = (UInt32Value)18U, DataType = CellValues.SharedString };
            //CellValue cellValue32 = new CellValue();
            //cellValue32.Text = "31";
            //cell89.Append(cellValue32);

            //Cell cell90 = new Cell() { CellReference = "N" + Convert.ToUInt32(footerShift), StyleIndex = (UInt32Value)18U, DataType = CellValues.SharedString };
            //CellValue cellValue33 = new CellValue();
            //cellValue33.Text = "32";
            //cell90.Append(cellValue33);

            //row7.Append(cell77);
            //row7.Append(cell78);
            //row7.Append(cell79);
            //row7.Append(cell80);
            //row7.Append(cell81);
            //row7.Append(cell82);
            //row7.Append(cell83);
            //row7.Append(cell84);
            //row7.Append(cell85);
            //row7.Append(cell86);
            //row7.Append(cell87);
            //row7.Append(cell88);
            //row7.Append(cell89);
            //row7.Append(cell90);




            Row row8 = new Row()
            {
                RowIndex = Convert.ToUInt32(footerShift + 1),
                Spans = new ListValue<StringValue>() { InnerText = "1:14" },
                Height = 29.25D,
                CustomHeight = true,
                DyDescent = 0.25D
            };

            Cell cell91 = new Cell() { CellReference = "A" + Convert.ToUInt32(footerShift + 1), StyleIndex = (UInt32Value)30U, DataType = CellValues.SharedString };
            CellValue cellValue34 = new CellValue();
            cellValue34.Text = "23";

            cell91.Append(cellValue34);
            Cell cell92 = new Cell() { CellReference = "B" + Convert.ToUInt32(footerShift + 1), StyleIndex = (UInt32Value)30U };
            Cell cell93 = new Cell() { CellReference = "C" + Convert.ToUInt32(footerShift + 1), StyleIndex = (UInt32Value)30U };
            Cell cell94 = new Cell() { CellReference = "D" + Convert.ToUInt32(footerShift + 1), StyleIndex = (UInt32Value)30U };
            Cell cell95 = new Cell() { CellReference = "E" + Convert.ToUInt32(footerShift + 1), StyleIndex = (UInt32Value)30U };
            Cell cell96 = new Cell() { CellReference = "F" + Convert.ToUInt32(footerShift + 1), StyleIndex = (UInt32Value)30U };
            Cell cell97 = new Cell() { CellReference = "G" + Convert.ToUInt32(footerShift + 1), StyleIndex = (UInt32Value)30U };
            Cell cell98 = new Cell() { CellReference = "H" + Convert.ToUInt32(footerShift + 1), StyleIndex = (UInt32Value)30U };
            Cell cell99 = new Cell() { CellReference = "I" + Convert.ToUInt32(footerShift + 1), StyleIndex = (UInt32Value)30U };
            Cell cell100 = new Cell() { CellReference = "J" + Convert.ToUInt32(footerShift + 1), StyleIndex = (UInt32Value)30U };
            Cell cell101 = new Cell() { CellReference = "K" + Convert.ToUInt32(footerShift + 1), StyleIndex = (UInt32Value)14U };
            Cell cell102 = new Cell() { CellReference = "L" + Convert.ToUInt32(footerShift + 1), StyleIndex = (UInt32Value)14U };
            Cell cell103 = new Cell() { CellReference = "M" + Convert.ToUInt32(footerShift + 1), StyleIndex = (UInt32Value)14U };
            Cell cell104 = new Cell() { CellReference = "N" + Convert.ToUInt32(footerShift + 1), StyleIndex = (UInt32Value)14U };

            row8.Append(cell91);
            row8.Append(cell92);
            row8.Append(cell93);
            row8.Append(cell94);
            row8.Append(cell95);
            row8.Append(cell96);
            row8.Append(cell97);
            row8.Append(cell98);
            row8.Append(cell99);
            row8.Append(cell100);
            row8.Append(cell101);
            row8.Append(cell102);
            row8.Append(cell103);
            row8.Append(cell104);


            Row row9 = new Row() { RowIndex = Convert.ToUInt32(footerShift + 2), Spans = new ListValue<StringValue>() { InnerText = "1:14" }, Height = 18.75D, CustomHeight = true, DyDescent = 0.25D };
            Cell cell105 = new Cell() { CellReference = "A" + Convert.ToUInt32(footerShift + 2), StyleIndex = (UInt32Value)38U };
            Cell cell106 = new Cell() { CellReference = "B" + Convert.ToUInt32(footerShift + 2), StyleIndex = (UInt32Value)39U };
            Cell cell107 = new Cell() { CellReference = "C" + Convert.ToUInt32(footerShift + 2), StyleIndex = (UInt32Value)39U };
            Cell cell108 = new Cell() { CellReference = "D" + Convert.ToUInt32(footerShift + 2), StyleIndex = (UInt32Value)39U };
            Cell cell109 = new Cell() { CellReference = "E" + Convert.ToUInt32(footerShift + 2), StyleIndex = (UInt32Value)39U };
            Cell cell110 = new Cell() { CellReference = "F" + Convert.ToUInt32(footerShift + 2), StyleIndex = (UInt32Value)39U };
            Cell cell111 = new Cell() { CellReference = "G" + Convert.ToUInt32(footerShift + 2), StyleIndex = (UInt32Value)39U };
            Cell cell112 = new Cell() { CellReference = "H" + Convert.ToUInt32(footerShift + 2), StyleIndex = (UInt32Value)39U };
            Cell cell113 = new Cell() { CellReference = "I" + Convert.ToUInt32(footerShift + 2), StyleIndex = (UInt32Value)39U };
            Cell cell114 = new Cell() { CellReference = "J" + Convert.ToUInt32(footerShift + 2), StyleIndex = (UInt32Value)39U };
            Cell cell115 = new Cell() { CellReference = "K" + Convert.ToUInt32(footerShift + 2), StyleIndex = (UInt32Value)39U };
            Cell cell116 = new Cell() { CellReference = "L" + Convert.ToUInt32(footerShift + 2), StyleIndex = (UInt32Value)39U };
            Cell cell117 = new Cell() { CellReference = "M" + Convert.ToUInt32(footerShift + 2), StyleIndex = (UInt32Value)39U };
            Cell cell118 = new Cell() { CellReference = "N" + Convert.ToUInt32(footerShift + 2), StyleIndex = (UInt32Value)39U };

            row9.Append(cell105);
            row9.Append(cell106);
            row9.Append(cell107);
            row9.Append(cell108);
            row9.Append(cell109);
            row9.Append(cell110);
            row9.Append(cell111);
            row9.Append(cell112);
            row9.Append(cell113);
            row9.Append(cell114);
            row9.Append(cell115);
            row9.Append(cell116);
            row9.Append(cell117);
            row9.Append(cell118);
            

            Row row10 = new Row() { RowIndex = Convert.ToUInt32(footerShift + 3), Spans = new ListValue<StringValue>() { InnerText = "1:14" }, Height = 29.25D, CustomHeight = true, DyDescent = 0.25D };

            Cell cell119 = new Cell() { CellReference = "A" + Convert.ToUInt32(footerShift + 3), StyleIndex = (UInt32Value)29U, DataType = CellValues.SharedString };
            CellValue cellValue35 = new CellValue();
            cellValue35.Text = "18";

            cell119.Append(cellValue35);
            Cell cell120 = new Cell() { CellReference = "B" + Convert.ToUInt32(footerShift + 3), StyleIndex = (UInt32Value)29U };
            Cell cell121 = new Cell() { CellReference = "C" + Convert.ToUInt32(footerShift + 3), StyleIndex = (UInt32Value)29U };
            Cell cell122 = new Cell() { CellReference = "D" + Convert.ToUInt32(footerShift + 3), StyleIndex = (UInt32Value)29U };
            Cell cell123 = new Cell() { CellReference = "E" + Convert.ToUInt32(footerShift + 3), StyleIndex = (UInt32Value)29U };
            Cell cell124 = new Cell() { CellReference = "F" + Convert.ToUInt32(footerShift + 3), StyleIndex = (UInt32Value)29U };
            Cell cell125 = new Cell() { CellReference = "G" + Convert.ToUInt32(footerShift + 3), StyleIndex = (UInt32Value)29U };
            Cell cell126 = new Cell() { CellReference = "H" + Convert.ToUInt32(footerShift + 3), StyleIndex = (UInt32Value)29U };
            Cell cell127 = new Cell() { CellReference = "I" + Convert.ToUInt32(footerShift + 3), StyleIndex = (UInt32Value)29U };
            Cell cell128 = new Cell() { CellReference = "J" + Convert.ToUInt32(footerShift + 3), StyleIndex = (UInt32Value)29U };
            Cell cell129 = new Cell() { CellReference = "K" + Convert.ToUInt32(footerShift + 3), StyleIndex = (UInt32Value)29U };
            Cell cell130 = new Cell() { CellReference = "L" + Convert.ToUInt32(footerShift + 3), StyleIndex = (UInt32Value)29U };
            Cell cell131 = new Cell() { CellReference = "M" + Convert.ToUInt32(footerShift + 3), StyleIndex = (UInt32Value)29U };
            Cell cell132 = new Cell() { CellReference = "N" + Convert.ToUInt32(footerShift + 3), StyleIndex = (UInt32Value)29U };

            row10.Append(cell119);
            row10.Append(cell120);
            row10.Append(cell121);
            row10.Append(cell122);
            row10.Append(cell123);
            row10.Append(cell124);
            row10.Append(cell125);
            row10.Append(cell126);
            row10.Append(cell127);
            row10.Append(cell128);
            row10.Append(cell129);
            row10.Append(cell130);
            row10.Append(cell131);
            row10.Append(cell132);




            Row row11 = new Row() { RowIndex = Convert.ToUInt32(footerShift + 4), Spans = new ListValue<StringValue>() { InnerText = "1:14" }, DyDescent = 0.25D };
            Cell cell133 = new Cell() { CellReference = "C" + Convert.ToUInt32(footerShift + 4), StyleIndex = (UInt32Value)3U };

            row11.Append(cell133);

            sheetData1.Append(row1);
            sheetData1.Append(row2);
            sheetData1.Append(row3);
            sheetData1.Append(row4);
            sheetData1.Append(row5);
            sheetData1.Append(row6);

            //ADD ROW DATA
            RowsData(sheetData1);

            //sheetData1.Append(row7);
            sheetData1.Append(row8);
            sheetData1.Append(row9);
            sheetData1.Append(row10);
            sheetData1.Append(row11);

            MergeCells mergeCells1 = new MergeCells() { Count = (UInt32Value)19U };
            MergeCell mergeCell2 = new MergeCell() { Reference = "F5:G5" };
            MergeCell mergeCell3 = new MergeCell() { Reference = "H5:J5" };
            MergeCell mergeCell4 = new MergeCell() { Reference = "A5:A6" };
            MergeCell mergeCell5 = new MergeCell() { Reference = "B5:B6" };
            MergeCell mergeCell6 = new MergeCell() { Reference = "D5:D6" };
            MergeCell mergeCell7 = new MergeCell() { Reference = "E5:E6" };
            MergeCell mergeCell8 = new MergeCell() { Reference = "C5:C6" };
            MergeCell mergeCell11 = new MergeCell() { Reference = "A1:J1" };
            MergeCell mergeCell12 = new MergeCell() { Reference = "B2:C2" };
            MergeCell mergeCell13 = new MergeCell() { Reference = "B3:C3" };
            MergeCell mergeCell14 = new MergeCell() { Reference = "A4:N4" };
            MergeCell mergeCell15 = new MergeCell() { Reference = "K5:K6" };
            MergeCell mergeCell16 = new MergeCell() { Reference = "L5:L6" };
            MergeCell mergeCell17 = new MergeCell() { Reference = "M5:M6" };
            MergeCell mergeCell18 = new MergeCell() { Reference = "N5:N6" };
            MergeCell mergeCell19 = new MergeCell() { Reference = "L3:N3" };

            MergeCell mergeCell20 = new MergeCell() { Reference = "A"  + Convert.ToUInt32(footerShift + 3) + ":N" + Convert.ToUInt32(footerShift + 4)  };
            MergeCell mergeCell21 = new MergeCell() { Reference = "A" + Convert.ToUInt32(footerShift + 1) + ":M" + Convert.ToUInt32(footerShift + 1) };

            mergeCells1.Append(mergeCell2);
            mergeCells1.Append(mergeCell3);
            mergeCells1.Append(mergeCell4);
            mergeCells1.Append(mergeCell5);
            mergeCells1.Append(mergeCell6);
            mergeCells1.Append(mergeCell7);
            mergeCells1.Append(mergeCell8);
            mergeCells1.Append(mergeCell11);
            mergeCells1.Append(mergeCell12);
            mergeCells1.Append(mergeCell13);
            mergeCells1.Append(mergeCell14);
            mergeCells1.Append(mergeCell15);
            mergeCells1.Append(mergeCell16);
            mergeCells1.Append(mergeCell17);
            mergeCells1.Append(mergeCell18);
            mergeCells1.Append(mergeCell19);

            mergeCells1.Append(mergeCell20);
            mergeCells1.Append(mergeCell21);


            PageMargins pageMargins1 = new PageMargins() { Left = 0.7D, Right = 0.7D, Top = 0.75D, Bottom = 0.75D, Header = 0.3D, Footer = 0.3D };
            PageSetup pageSetup1 = new PageSetup() { Orientation = OrientationValues.Portrait, HorizontalDpi = (UInt32Value)300U, VerticalDpi = (UInt32Value)200U, Id = "rId1" };
            Drawing drawing1 = new Drawing() { Id = "rId2" };

            worksheet1.Append(sheetDimension1);
            worksheet1.Append(sheetViews1);
            worksheet1.Append(sheetFormatProperties1);
            worksheet1.Append(columns1);
            worksheet1.Append(sheetData1);
            worksheet1.Append(mergeCells1);
            worksheet1.Append(pageMargins1);
            worksheet1.Append(pageSetup1);
            worksheet1.Append(drawing1);

            worksheetPart1.Worksheet = worksheet1;
        }

        // Generates content of drawingsPart1.
        private void GenerateDrawingsPart1Content(DrawingsPart drawingsPart1)
        {
            Xdr.WorksheetDrawing worksheetDrawing1 = new Xdr.WorksheetDrawing();
            worksheetDrawing1.AddNamespaceDeclaration("xdr", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
            worksheetDrawing1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

            Xdr.TwoCellAnchor twoCellAnchor1 = new Xdr.TwoCellAnchor();

            Xdr.FromMarker fromMarker1 = new Xdr.FromMarker();
            Xdr.ColumnId columnId1 = new Xdr.ColumnId();
            columnId1.Text = "0";
            Xdr.ColumnOffset columnOffset1 = new Xdr.ColumnOffset();
            columnOffset1.Text = "57150";
            Xdr.RowId rowId1 = new Xdr.RowId();
            rowId1.Text = "0";
            Xdr.RowOffset rowOffset1 = new Xdr.RowOffset();
            rowOffset1.Text = "38100";

            fromMarker1.Append(columnId1);
            fromMarker1.Append(columnOffset1);
            fromMarker1.Append(rowId1);
            fromMarker1.Append(rowOffset1);

            Xdr.ToMarker toMarker1 = new Xdr.ToMarker();
            Xdr.ColumnId columnId2 = new Xdr.ColumnId();
            columnId2.Text = "1";
            Xdr.ColumnOffset columnOffset2 = new Xdr.ColumnOffset();
            columnOffset2.Text = "1162050";
            Xdr.RowId rowId2 = new Xdr.RowId();
            rowId2.Text = "0";
            Xdr.RowOffset rowOffset2 = new Xdr.RowOffset();
            rowOffset2.Text = "1076325";

            toMarker1.Append(columnId2);
            toMarker1.Append(columnOffset2);
            toMarker1.Append(rowId2);
            toMarker1.Append(rowOffset2);

            Xdr.Picture picture1 = new Xdr.Picture();

            Xdr.NonVisualPictureProperties nonVisualPictureProperties1 = new Xdr.NonVisualPictureProperties();
            Xdr.NonVisualDrawingProperties nonVisualDrawingProperties1 = new Xdr.NonVisualDrawingProperties() { Id = (UInt32Value)12U, Name = "Picture 2" };

            Xdr.NonVisualPictureDrawingProperties nonVisualPictureDrawingProperties1 = new Xdr.NonVisualPictureDrawingProperties();
            A.PictureLocks pictureLocks1 = new A.PictureLocks() { NoChangeAspect = true, NoChangeArrowheads = true };

            nonVisualPictureDrawingProperties1.Append(pictureLocks1);

            nonVisualPictureProperties1.Append(nonVisualDrawingProperties1);
            nonVisualPictureProperties1.Append(nonVisualPictureDrawingProperties1);

            Xdr.BlipFill blipFill1 = new Xdr.BlipFill();

            A.Blip blip1 = new A.Blip() { Embed = "rId1", CompressionState = A.BlipCompressionValues.Print };
            blip1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            A.SourceRectangle sourceRectangle1 = new A.SourceRectangle();

            A.Stretch stretch1 = new A.Stretch();
            A.FillRectangle fillRectangle1 = new A.FillRectangle();

            stretch1.Append(fillRectangle1);

            blipFill1.Append(blip1);
            blipFill1.Append(sourceRectangle1);
            blipFill1.Append(stretch1);

            Xdr.ShapeProperties shapeProperties1 = new Xdr.ShapeProperties() { BlackWhiteMode = A.BlackWhiteModeValues.Auto };

            A.Transform2D transform2D1 = new A.Transform2D();
            A.Offset offset1 = new A.Offset() { X = 57150L, Y = 38100L };
            A.Extents extents1 = new A.Extents() { Cx = 2181225L, Cy = 1038225L };

            transform2D1.Append(offset1);
            transform2D1.Append(extents1);

            A.PresetGeometry presetGeometry1 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList1 = new A.AdjustValueList();

            presetGeometry1.Append(adjustValueList1);
            A.NoFill noFill1 = new A.NoFill();

            A.Outline outline4 = new A.Outline() { Width = 9525 };
            A.NoFill noFill2 = new A.NoFill();
            A.Miter miter1 = new A.Miter() { Limit = 800000 };
            A.HeadEnd headEnd1 = new A.HeadEnd();
            A.TailEnd tailEnd1 = new A.TailEnd();

            outline4.Append(noFill2);
            outline4.Append(miter1);
            outline4.Append(headEnd1);
            outline4.Append(tailEnd1);

            shapeProperties1.Append(transform2D1);
            shapeProperties1.Append(presetGeometry1);
            shapeProperties1.Append(noFill1);
            shapeProperties1.Append(outline4);

            picture1.Append(nonVisualPictureProperties1);
            picture1.Append(blipFill1);
            picture1.Append(shapeProperties1);
            Xdr.ClientData clientData1 = new Xdr.ClientData();

            twoCellAnchor1.Append(fromMarker1);
            twoCellAnchor1.Append(toMarker1);
            twoCellAnchor1.Append(picture1);
            twoCellAnchor1.Append(clientData1);

            worksheetDrawing1.Append(twoCellAnchor1);

            RowsImage(worksheetDrawing1);

            drawingsPart1.WorksheetDrawing = worksheetDrawing1;
        }

        // Generates content of imagePart1.
        private void GenerateImagePart1Content(ImagePart imagePart1)
        {
            System.IO.Stream data = GetBinaryDataStream(imagePart1Data);
            imagePart1.FeedData(data);
            data.Close();
        }

        // Generates content of spreadsheetPrinterSettingsPart1.
        private void GenerateSpreadsheetPrinterSettingsPart1Content(SpreadsheetPrinterSettingsPart spreadsheetPrinterSettingsPart1)
        {
            System.IO.Stream data = GetBinaryDataStream(spreadsheetPrinterSettingsPart1Data);
            spreadsheetPrinterSettingsPart1.FeedData(data);
            data.Close();
        }

        // Generates content of sharedStringTablePart1.
        private void GenerateSharedStringTablePart1Content(SharedStringTablePart sharedStringTablePart1)
        {
            SharedStringTable sharedStringTable1 = new SharedStringTable() { Count = (UInt32Value)35U, UniqueCount = (UInt32Value)33U };

            SharedStringItem sharedStringItem1 = new SharedStringItem();
            Text text1 = new Text();
            text1.Text = "CODE NO";

            sharedStringItem1.Append(text1);

            SharedStringItem sharedStringItem2 = new SharedStringItem();
            Text text2 = new Text();
            text2.Text = "PICTURE";

            sharedStringItem2.Append(text2);

            SharedStringItem sharedStringItem3 = new SharedStringItem();
            Text text3 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text3.Text = "DESCRIPTION ";

            sharedStringItem3.Append(text3);

            SharedStringItem sharedStringItem4 = new SharedStringItem();
            Text text4 = new Text();
            text4.Text = "SIZE (CM)";

            sharedStringItem4.Append(text4);

            SharedStringItem sharedStringItem5 = new SharedStringItem();
            Text text5 = new Text();
            text5.Text = "PACKING";

            sharedStringItem5.Append(text5);

            SharedStringItem sharedStringItem6 = new SharedStringItem();
            Text text6 = new Text();
            text6.Text = "PCS/SE";

            sharedStringItem6.Append(text6);

            SharedStringItem sharedStringItem7 = new SharedStringItem();
            Text text7 = new Text();
            text7.Text = "CBM";

            sharedStringItem7.Append(text7);

            SharedStringItem sharedStringItem8 = new SharedStringItem();
            Text text8 = new Text();
            text8.Text = "W";

            sharedStringItem8.Append(text8);

            SharedStringItem sharedStringItem9 = new SharedStringItem();
            Text text9 = new Text();
            text9.Text = "D";

            sharedStringItem9.Append(text9);

            SharedStringItem sharedStringItem10 = new SharedStringItem();
            Text text10 = new Text();
            text10.Text = "H";

            sharedStringItem10.Append(text10);

            SharedStringItem sharedStringItem11 = new SharedStringItem();
            Text text11 = new Text();
            text11.Text = "PRICE FOB HAIPHONG-VIETNAM\nUSD";

            sharedStringItem11.Append(text11);

            SharedStringItem sharedStringItem12 = new SharedStringItem();
            Text text12 = new Text();
            text12.Text = "CARTON MEASUREMENT\n (CM)";

            sharedStringItem12.Append(text12);

            SharedStringItem sharedStringItem13 = new SharedStringItem();
            Text text13 = new Text();
            text13.Text = "QUOTATION";

            sharedStringItem13.Append(text13);

            SharedStringItem sharedStringItem14 = new SharedStringItem();
            Text text14 = new Text();
            text14.Text = "COMPANY:";

            sharedStringItem14.Append(text14);

            SharedStringItem sharedStringItem15 = new SharedStringItem();
            Text text15 = new Text();
            text15.Text = "ATTN:";

            sharedStringItem15.Append(text15);

            SharedStringItem sharedStringItem16 = new SharedStringItem();
            Text text16 = new Text();
            text16.Text = "DATE:";

            sharedStringItem16.Append(text16);

            SharedStringItem sharedStringItem17 = new SharedStringItem();

            Run run1 = new Run();

            RunProperties runProperties1 = new RunProperties();
            Bold bold12 = new Bold();
            FontSize fontSize18 = new FontSize() { Val = 20D };
            DocumentFormat.OpenXml.Spreadsheet.Color color37 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Rgb = "FFA47900" };
            RunFont runFont1 = new RunFont() { Val = "Brush Script MT" };
            FontFamily fontFamily1 = new FontFamily() { Val = 4 };

            runProperties1.Append(bold12);
            runProperties1.Append(fontSize18);
            runProperties1.Append(color37);
            runProperties1.Append(runFont1);
            runProperties1.Append(fontFamily1);
            Text text17 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text17.Text = "                           ";

            run1.Append(runProperties1);
            run1.Append(text17);

            Run run2 = new Run();

            RunProperties runProperties2 = new RunProperties();
            Bold bold13 = new Bold();
            FontSize fontSize19 = new FontSize() { Val = 24D };
            DocumentFormat.OpenXml.Spreadsheet.Color color38 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Rgb = "FFA47900" };
            RunFont runFont2 = new RunFont() { Val = "Brush Script MT" };
            FontFamily fontFamily2 = new FontFamily() { Val = 4 };

            runProperties2.Append(bold13);
            runProperties2.Append(fontSize19);
            runProperties2.Append(color38);
            runProperties2.Append(runFont2);
            runProperties2.Append(fontFamily2);
            Text text18 = new Text();
            text18.Text = "MK Handicrafts Co., Ltd";

            run2.Append(runProperties2);
            run2.Append(text18);

            Run run3 = new Run();

            RunProperties runProperties3 = new RunProperties();
            FontSize fontSize20 = new FontSize() { Val = 11D };
            DocumentFormat.OpenXml.Spreadsheet.Color color39 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Theme = (UInt32Value)1U };
            RunFont runFont3 = new RunFont() { Val = "Calibri" };
            FontFamily fontFamily3 = new FontFamily() { Val = 2 };
            FontScheme fontScheme5 = new FontScheme() { Val = FontSchemeValues.Minor };

            runProperties3.Append(fontSize20);
            runProperties3.Append(color39);
            runProperties3.Append(runFont3);
            runProperties3.Append(fontFamily3);
            runProperties3.Append(fontScheme5);
            Text text19 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text19.Text = "\n                                                                            ";

            run3.Append(runProperties3);
            run3.Append(text19);

            Run run4 = new Run();

            RunProperties runProperties4 = new RunProperties();
            FontSize fontSize21 = new FontSize() { Val = 11D };
            DocumentFormat.OpenXml.Spreadsheet.Color color40 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Theme = (UInt32Value)1U };
            RunFont runFont4 = new RunFont() { Val = "Arial" };
            FontFamily fontFamily4 = new FontFamily() { Val = 2 };

            runProperties4.Append(fontSize21);
            runProperties4.Append(color40);
            runProperties4.Append(runFont4);
            runProperties4.Append(fontFamily4);
            Text text20 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text20.Text = "    ";

            run4.Append(runProperties4);
            run4.Append(text20);

            Run run5 = new Run();

            RunProperties runProperties5 = new RunProperties();
            FontSize fontSize22 = new FontSize() { Val = 12D };
            DocumentFormat.OpenXml.Spreadsheet.Color color41 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Theme = (UInt32Value)1U };
            RunFont runFont5 = new RunFont() { Val = "Arial" };
            FontFamily fontFamily5 = new FontFamily() { Val = 2 };

            runProperties5.Append(fontSize22);
            runProperties5.Append(color41);
            runProperties5.Append(runFont5);
            runProperties5.Append(fontFamily5);
            Text text21 = new Text();
            text21.Text = "Lot A5-5 + A5-6 Green Island Villas, Hai Chau District,  Danang City, Vietnam\n                                                             Tel :  84.511.3623727     Fax:  84.511.3623717\n                                                             Email: mkhandicrafts2014@gmail.com";

            run5.Append(runProperties5);
            run5.Append(text21);

            sharedStringItem17.Append(run1);
            sharedStringItem17.Append(run2);
            sharedStringItem17.Append(run3);
            sharedStringItem17.Append(run4);
            sharedStringItem17.Append(run5);

            SharedStringItem sharedStringItem18 = new SharedStringItem();
            Text text22 = new Text();
            text22.Text = "";

            sharedStringItem18.Append(text22);

            SharedStringItem sharedStringItem19 = new SharedStringItem();
            Text text23 = new Text();
            text23.Text = "THANK YOU FOR YOUR KIND ATTENTION";

            sharedStringItem19.Append(text23);

            SharedStringItem sharedStringItem20 = new SharedStringItem();
            Text text24 = new Text();
            text24.Text = "ORDER QTY";

            sharedStringItem20.Append(text24);

            SharedStringItem sharedStringItem21 = new SharedStringItem();
            Text text25 = new Text();
            text25.Text = "CARTON QTY";

            sharedStringItem21.Append(text25);

            SharedStringItem sharedStringItem22 = new SharedStringItem();
            Text text26 = new Text();
            text26.Text = "TOTAL CBM";

            sharedStringItem22.Append(text26);

            SharedStringItem sharedStringItem23 = new SharedStringItem();
            Text text27 = new Text();
            text27.Text = "AMOUNT";

            sharedStringItem23.Append(text27);

            SharedStringItem sharedStringItem24 = new SharedStringItem();
            Text text28 = new Text();
            text28.Text = "TOTAL";

            sharedStringItem24.Append(text28);

            SharedStringItem sharedStringItem25 = new SharedStringItem();
            Text text29 = new Text();
            text29.Text = "A";

            sharedStringItem25.Append(text29);

            SharedStringItem sharedStringItem26 = new SharedStringItem();
            Text text30 = new Text();
            text30.Text = "B";

            sharedStringItem26.Append(text30);

            SharedStringItem sharedStringItem27 = new SharedStringItem();
            Text text31 = new Text();
            text31.Text = "C";

            sharedStringItem27.Append(text31);

            SharedStringItem sharedStringItem28 = new SharedStringItem();
            Text text32 = new Text();
            text32.Text = "E";

            sharedStringItem28.Append(text32);

            SharedStringItem sharedStringItem29 = new SharedStringItem();
            Text text33 = new Text();
            text33.Text = "F";

            sharedStringItem29.Append(text33);

            SharedStringItem sharedStringItem30 = new SharedStringItem();
            Text text34 = new Text();
            text34.Text = "I";

            sharedStringItem30.Append(text34);

            SharedStringItem sharedStringItem31 = new SharedStringItem();
            Text text35 = new Text();
            text35.Text = "J";

            sharedStringItem31.Append(text35);

            SharedStringItem sharedStringItem32 = new SharedStringItem();
            Text text36 = new Text();
            text36.Text = "K";

            sharedStringItem32.Append(text36);

            SharedStringItem sharedStringItem33 = new SharedStringItem();
            Text text37 = new Text();
            text37.Text = "L";

            sharedStringItem33.Append(text37);

            sharedStringTable1.Append(sharedStringItem1);
            sharedStringTable1.Append(sharedStringItem2);
            sharedStringTable1.Append(sharedStringItem3);
            sharedStringTable1.Append(sharedStringItem4);
            sharedStringTable1.Append(sharedStringItem5);
            sharedStringTable1.Append(sharedStringItem6);
            sharedStringTable1.Append(sharedStringItem7);
            sharedStringTable1.Append(sharedStringItem8);
            sharedStringTable1.Append(sharedStringItem9);
            sharedStringTable1.Append(sharedStringItem10);
            sharedStringTable1.Append(sharedStringItem11);
            sharedStringTable1.Append(sharedStringItem12);
            sharedStringTable1.Append(sharedStringItem13);
            sharedStringTable1.Append(sharedStringItem14);
            sharedStringTable1.Append(sharedStringItem15);
            sharedStringTable1.Append(sharedStringItem16);
            sharedStringTable1.Append(sharedStringItem17);
            sharedStringTable1.Append(sharedStringItem18);
            sharedStringTable1.Append(sharedStringItem19);
            sharedStringTable1.Append(sharedStringItem20);
            sharedStringTable1.Append(sharedStringItem21);
            sharedStringTable1.Append(sharedStringItem22);
            sharedStringTable1.Append(sharedStringItem23);
            sharedStringTable1.Append(sharedStringItem24);
            sharedStringTable1.Append(sharedStringItem25);
            sharedStringTable1.Append(sharedStringItem26);
            sharedStringTable1.Append(sharedStringItem27);
            sharedStringTable1.Append(sharedStringItem28);
            sharedStringTable1.Append(sharedStringItem29);
            sharedStringTable1.Append(sharedStringItem30);
            sharedStringTable1.Append(sharedStringItem31);
            sharedStringTable1.Append(sharedStringItem32);
            sharedStringTable1.Append(sharedStringItem33);

            sharedStringTablePart1.SharedStringTable = sharedStringTable1;
        }

        private void SetPackageProperties(OpenXmlPackage document)
        {
            document.PackageProperties.Creator = "KevinPham";
            document.PackageProperties.Created = System.Xml.XmlConvert.ToDateTime("2015-08-12T04:29:28Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
            document.PackageProperties.Modified = System.Xml.XmlConvert.ToDateTime("2016-08-01T14:35:16Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
            document.PackageProperties.LastModifiedBy = "Linh Le";
        }

        #region Binary Data
        private string imagePart1Data = "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";
        private string imagePart3Data = "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";

        private string spreadsheetPrinterSettingsPart1Data = "TQBpAGMAcgBvAHMAbwBmAHQAIABYAFAAUwAgAEQAbwBjAHUAbQBlAG4AdAAgAFcAcgBpAHQAZQByAAAAAAAAAAEEAwbcAJgDA68AAAEAAQCaCzQIZAABAA8AWAICAAEAWAIDAAAAQQA0AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAAAAABAAAAAgAAAAEAAAD/////R0lTNAAAAAAAAAAAAAAAAERJTlUiACABfAMcAMrS9nIAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACQAAAAAAAAAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgAQAAU01USgAAAAAQABABewAwAEYANAAxADMAMABEAEQALQAxADkAQwA3AC0ANwBhAGIANgAtADkAOQBBADEALQA5ADgAMABGADAAMwBCADIARQBFADQARQB9AAAASW5wdXRCaW4ARk9STVNPVVJDRQBSRVNETEwAVW5pcmVzRExMAEludGVybGVhdmluZwBPRkYASW1hZ2VUeXBlAEpQRUdNZWQAT3JpZW50YXRpb24AUE9SVFJBSVQAQ29sbGF0ZQBPRkYAUmVzb2x1dGlvbgBPcHRpb24xAFBhcGVyU2l6ZQBMRVRURVIAQ29sb3JNb2RlADI0YnBwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAcAAAAVjRETQEAAAAAAAAAAAAAAAAAAAAAAAAA";

        private System.IO.Stream GetBinaryDataStream(string base64String)
        {
            return new System.IO.MemoryStream(System.Convert.FromBase64String(base64String));
        }

        #endregion


    }
}