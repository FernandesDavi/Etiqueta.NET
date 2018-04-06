using BarcodeLib;
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

        public enum Tags
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

            var bmp = GetTemplate(CorreiosTag.Tags.Quatro);
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
            var tag = new Tag
            {
                Contrato = 1234567890,
                Servico = "Sedex Hoje Amanha",
                NF = 1234567,
                Pedido = "123456789",
                Peso = 123456,
                Rastreamento = "PP578197848BR",

                Destinatario = "Teste teste teste teste teste teste teste teste t",
                EnderecoDest = "Rua Deputado Emilio Carlos",
                NumeroDest = 117111,
                ComplementoDest = "dsdasda",
                BairroDest = "Jd Silveira",
                CEPDest = "12345-678",
                CidadeDest = "Vila Bela da Santíssima Trindade",
                UFDest = "SP",

                Remetente = "teste2 teste2 teste2 teste2 teste2 teste2 teste2 2",
                EndrerecoRemet = "Rua Deputado Emilio Carlos",
                NumeroRemet = 123456,
                ComplementoRemet = "cdasdasdasdas",
                BairroRemet = "Jd Silveira",
                CEPRemet = "12345-678",
                CidadeRemet = "Vila Bela da Santíssima Trindade",
                UFRemet = "SP"

            };

            var bmp = GetTemplate(CorreiosTag.Tags.Seis);
            var Matrix = GenerateDataMatrix("064340300111706434070001170000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            tag.CEPDest = tag.CEPDest.Replace("-", string.Empty);
            var icon = GetType(CorreiosTag.TagType.PREMIUM);
            var imgCodigoBarrasZip = GerarCodBarrasCep(tag.CEPDest);
            var imgCodBarraRastreamento = GerarCodBarrasRegistro(tag.Rastreamento);
            var codRegistroPoint = new RectangleF(338, 10, 59, 59);
            var dataMatrix = new RectangleF(10, 10, 90, 90);
            var contratoXY = new RectangleF(155, 38, 200, 22);
            var servicoXY = new RectangleF(100, 50, 200, 22);
            var nfXY = new RectangleF(250, 8, 200, 22);
            var pedidoXY = new RectangleF(271, 23, 200, 22);
            var pesoXY = new RectangleF(282, 53, 200, 22);
            var rastreamentoXY = new RectangleF(100, 87, 200, 22);
            var destinatarioXY = new RectangleF(10, 230, 300, 40);
            var imgCodbarraXY = new RectangleF(250, 220, 140, 60);

            var endereDestXY = new RectangleF(10, 245, 270, 60);
            var cepDestXY = new RectangleF(10, 274, 270, 60);
            var cidadeXY = new RectangleF(75, 274, 270, 60);
            var RemetenteStatic = new RectangleF(10, 295, 270, 60);
            var remetenteXY = new RectangleF(75, 295, 270, 60);
            var enderecoRemetXY = new RectangleF(10, 310, 430, 60);
            var cepRemeXY = new RectangleF(10, 323, 430, 60);
            var cidadeRemXY = new RectangleF(70, 323, 430, 100);
            var codBarraRastreamentoXY = new RectangleF(30, 105, 310, 75);

            var g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.DrawImage(icon, codRegistroPoint);
            g.DrawImage(Matrix, dataMatrix);
            g.DrawImage(imgCodigoBarrasZip, imgCodbarraXY);
            g.DrawImage(imgCodBarraRastreamento, codBarraRastreamentoXY);
            tag.EnderecoDest += ", " + tag.NumeroDest + ", " + tag.ComplementoDest + " " + tag.CidadeDest;
            tag.CidadeDest += "/" + tag.UFDest;
            tag.EndrerecoRemet += ", " + tag.NumeroRemet + " - " + tag.ComplementoRemet + " - " + tag.BairroRemet;
            tag.CidadeRemet += "/" + tag.UFRemet;
            using (var font = new Font("Arial", 9, FontStyle.Bold))
            {
                g.DrawString(tag.Contrato.ToString(), font, Brushes.Black, contratoXY);
                g.DrawString(tag.Servico.ToString(), font, Brushes.Black, servicoXY);
                g.DrawString(tag.Rastreamento.ToString(), font, Brushes.Black, rastreamentoXY);
                g.DrawString(tag.CEPDest.ToString(), font, Brushes.Black, cepDestXY);

            }

            using (var font = new Font("Arial", 9, FontStyle.Regular))
            {
                g.DrawString(tag.NF.ToString(), font, Brushes.Black, nfXY);
                g.DrawString(tag.Pedido.ToString(), font, Brushes.Black, pedidoXY);
                g.DrawString(tag.Peso.ToString(), font, Brushes.Black, pesoXY);
                g.DrawString(tag.Destinatario.ToString(), font, Brushes.Black, destinatarioXY);
                g.DrawString(tag.EnderecoDest.ToString(), font, Brushes.Black, endereDestXY);
                g.DrawString(tag.CidadeDest.ToString(), font, Brushes.Black, cidadeXY);

            }
            using (var font = new Font("Arial", 8, FontStyle.Bold))
            {
                g.DrawString("Remetente:", font, Brushes.Black, RemetenteStatic);
                g.DrawString(tag.CEPRemet, font, Brushes.Black, cepRemeXY);

            }
            using (var font = new Font("Arial", 8, FontStyle.Regular))
            {
                g.DrawString(tag.Remetente.ToString(), font, Brushes.Black, remetenteXY);
                g.DrawString(tag.EndrerecoRemet.ToString(), font, Brushes.Black, enderecoRemetXY);
                g.DrawString(tag.CidadeRemet.ToString(), font, Brushes.Black, cidadeRemXY);
            }
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
                ModuleSize = 4, //3 =22,8mm; 4 = 30mm
                MarginSize = 0,
                BackColor = Color.White,
                ForeColor = Color.Black,
                Scheme = DmtxScheme.DmtxSchemeAsciiGS1
            };
            return encoder.EncodeImage(Code, options);
        }

        private static Bitmap GetTemplate(Tags tag)
        {
            Bitmap bmp = null;
            switch (tag)
            {
                case Tags.Quatro:
                    {
                        bmp = Properties.Resources.EtiquetaAutomatizada_4pFolha; break;
                    }
                case Tags.Seis:
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

        private static Image GerarCodBarrasCep(String cep)
        {
            using (var barcode = new Barcode
            {
                Alignment = AlignmentPositions.CENTER,
                Width = 250,//largura 
                Height = 180,//altura
                RotateFlipType = RotateFlipType.RotateNoneFlipNone,
                BackColor = Color.White,
                ForeColor = Color.Black
            })
            {
                var img = barcode.Encode(TYPE.CODE128A, cep);
                return img;
            }
        }

        private static Image GerarCodBarrasRegistro(String registro)
        {
            using (var barcode = new Barcode
            {
                Alignment = AlignmentPositions.CENTER,
                Width = 360,
                Height = 70,
                RotateFlipType = RotateFlipType.RotateNoneFlipNone,
                BackColor = Color.White,
                ForeColor = Color.Black,
            })
            {
                var img = barcode.Encode(TYPE.CODE128A, registro);
                return img;
            }
        }
        #endregion
    }
}
