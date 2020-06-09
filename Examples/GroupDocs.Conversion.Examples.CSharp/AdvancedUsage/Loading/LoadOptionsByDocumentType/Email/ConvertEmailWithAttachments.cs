﻿using System;
using System.IO;
using GroupDocs.Conversion.Options.Convert;
using GroupDocs.Conversion.Options.Load;

namespace GroupDocs.Conversion.Examples.CSharp.AdvancedUsage
{
    /// <summary>
    /// This example demonstrates how to convert an email document along with all attachments
    /// </summary>
    internal static class ConvertEmailWithAttachments
    {
        public static void Run()
        {
            string outputFolder = Constants.GetOutputDirectoryPath();
#if NETCOREAPP
            Func<LoadOptions> getLoadOptions = () => new EmailLoadOptions
            {
                ConvertOwner = true,
                ConvertOwned = true
            };
#else
            Contracts.Func<LoadOptions> getLoadOptions = () => new EmailLoadOptions
            {
                ConvertOwner = true,
                ConvertOwned = true
            };
#endif
            using (Converter converter = new Converter(Constants.SAMPLE_EML_WITH_ATTACHMENT, getLoadOptions))
            {
                int index = 1;
                PdfConvertOptions options = new PdfConvertOptions();
                converter.Convert(() =>
                {
                    string fileName = index == 1 ? "converted.pdf" : $"converted-attachment-{index - 1}.pdf";
                    index++;
                    string outputFile = Path.Combine(outputFolder, fileName);
                    return new FileStream(outputFile, FileMode.Create);
                }, options);
            }

            Console.WriteLine("\nEmail document converted successfully. \nCheck output in {0}", outputFolder);
        }
    }
}