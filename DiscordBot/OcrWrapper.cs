using DuoVia.FuzzyStrings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace DiscordBot
{
    class OcrWrapper
    {
        public static List<TextPoint> GetTextPointsFromFile(string fileName, string target = null, float imageScale = 1f)
        {
            List<TextPoint> result = new List<TextPoint>();

            using (TesseractEngine engine = new TesseractEngine(@"C:\PRCl_C#\DiscordBot\DiscordBot\tessdata", "eng", EngineMode.Default))
            {
                using (Pix img = Pix.LoadFromFile(fileName))
                {
                    using (Page page = engine.Process(img))
                    {
                        string text = page.GetText();
                        //Console.WriteLine("Mean confidence: {0}", page.GetMeanConfidence());

                        //Console.WriteLine("Text (GetText): \r\n{0}", text);
                        //Console.WriteLine("Text (iterator):");
                        using (ResultIterator iter = page.GetIterator())
                        {
                            iter.Begin();

                            do
                            {
                                do
                                {
                                    do
                                    {
                                        do
                                        {
                                            string curText = iter.GetText(PageIteratorLevel.Word);

                                            if (target == null || DoesTextContain(curText, target))
                                            {
                                                if (iter.TryGetBoundingBox(PageIteratorLevel.Word, out var rect))
                                                {
                                                    result.Add(new TextPoint()
                                                    {
                                                        Text = curText,
                                                        Point = new Point((int)(rect.X1 / imageScale), (int)(rect.Y1 / imageScale)),
                                                        BoundingBox = new Rectangle((int)(rect.X1 / imageScale), (int)(rect.Y1 / imageScale), (int)(rect.Width / imageScale), (int)(rect.Height / imageScale)),
                                                    });
                                                }
                                            }
                                        } while (iter.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));
                                    } while (iter.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));
                                } while (iter.Next(PageIteratorLevel.Block, PageIteratorLevel.Para));
                            } while (iter.Next(PageIteratorLevel.Block));
                        }
                    }
                }
            }

            return result;
        }

        private static bool DoesTextContain(string text, string target)
        {
            int subStringLength = target.Length;

            for (int i = 0; i < text.Length; i++)
            {
                string subString = "";

                for (int j = 0; j < subStringLength; j++)
                {
                    int index = i + j;

                    if (text.Length > index)
                    {
                        subString += text[index];
                    }
                }

                double match = subString.FuzzyMatch(target);

                if (match > 0.33)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
