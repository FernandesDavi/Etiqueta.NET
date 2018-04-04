using DataMatrix.net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etiqueta.NET.Core
{
    public class CorreiosTag
    {
        #region Properties
        public IList<String> PathsList { get; set; }

        public enum Tag
        {
            Quatro,
            Seis
        }

        public enum TagType
        {
            /// <summary>
            /// PREMIUM:
            /// SEDEX Hoje
            /// SEDEX 10
            /// SEDEX 12
            /// </summary>
            PREMIUM,
            /// <summary>
            /// EXPRESSA:
            /// SEDEX
            /// </summary>
            EXPRESSA,
            /// <summary>
            /// STANDARD:
            /// PAC
            /// </summary>
            STANDARD
        }
        #endregion
        #region Constructor

        #endregion

        #region Public Methods
        public static void GenerateFour()
        {

            var bmp = GetTemplate(CorreiosTag.Tag.Quatro);
            var icon = GetType(CorreiosTag.TagType.EXPRESSA);
            var codRegistroPoint = new RectangleF(320, 15, 96, 96);



            var g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            var format = "{0}\n{1}\n{2}, {3}\n{4}  {5} - {6}";
            g.DrawImage(icon, codRegistroPoint);
            //g.DrawString("dsadaas", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, codRegistroPoint);
            var fileName = "label-" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss-fff") + ".jpg";
            g.Flush();
            var path = fileName;
            bmp.Save(path);
            bmp.Dispose();
          
        }
        public static void GenerateSix()
        {

            var bmp = GetTemplate(CorreiosTag.Tag.Seis);
            var Matrix = GenerateDataMatrix("064340300111706434070001170000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            var icon = GetType(CorreiosTag.TagType.PREMIUM);
            var codRegistroPoint = new RectangleF(330, 10, 59, 59);
            var dataMatrix = new RectangleF(10, 10, 70, 70);
            


            var g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            var format = "{0}\n{1}\n{2}, {3}\n{4}  {5} - {6}";
            g.DrawImage(icon, codRegistroPoint);
            g.DrawImage(Matrix, dataMatrix);
            //g.DrawString("dsadaas", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, codRegistroPoint);
            var fileName = "label-" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss-fff") + ".jpg";
            g.Flush();
            var path = fileName;
            bmp.Save(path);
            bmp.Dispose();

        }
        #endregion

        #region Private Methods

        private static string GerarValidateCep(string cep)
        {
            var Validador = 0;
            var soma = 0;

            cep = cep.Replace("0", string.Empty);

            foreach (var item in cep)
            {
                soma += Convert.ToInt32(item);
            }
            if (soma % 10 == 0)
            {
                Validador = 0;
            }
            else
            {
                var i = soma;
                while (i % 10 != 0)
                {
                    i++;
                }
                Validador = i - soma;
            }
            return Validador.ToString();
        }

        private static Bitmap GenerateDataMatrix(string Code)
        {
            var encoder = new DmtxImageEncoder();
            var options = new DmtxImageEncoderOptions
            {
                ModuleSize = 3, //3 =22,8mm; 4 = 30mm
                MarginSize = 0,
                BackColor = Color.White,
                ForeColor = Color.Black,
                Scheme = DmtxScheme.DmtxSchemeAsciiGS1
            };
            return encoder.EncodeImage(Code, options);
        }

        private static Bitmap GetTemplate(Tag tag)
        {
        Bitmap bmp = null;
            switch (tag)
            {
                case Tag.Quatro:
                    {
                        bmp = Properties.Resources.EtiquetaAutomatizada_4pFolha; break;
                    }
                case Tag.Seis:
                    {
                        bmp = Properties.Resources.EtiquetaAutomatizada_6pFolha; break;
                    }
            }
            return bmp;
        }

        private static Bitmap GetType(TagType type)
        {
            Bitmap bmp = null;
            switch (type)
            {
                case TagType.EXPRESSA:
                    {
                        bmp = Properties.Resources.EXPRESSA; break;
                    }
                case TagType.PREMIUM:
                    {
                        bmp = Properties.Resources.Premium; break;
                    }
                case TagType.STANDARD:
                    {
                        bmp = Properties.Resources.pac; break;
                    }
            }
            return bmp;
        }

        #endregion
    }
}
