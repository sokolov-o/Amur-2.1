using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FERHRI.Common;
using Npgsql;

namespace FERHRI.Amur.Report
{
    public class ImageRepository : BaseRepository<Image>
    {
        public ImageRepository(Common.ADbNpgsql db) : base(db, "report.image") {}

        protected override object ParseData(NpgsqlDataReader reader)
        {
            return new Image(
                (int)reader["id"],
                new MemoryStream(reader["img"] as byte[])
            );
        }

        public static string ConvertImageToBase64(Stream img)
        {
            byte[] imageArray;

            imageArray = new byte[img.Length];
            img.Seek(0, System.IO.SeekOrigin.Begin);
            img.Read(imageArray, 0, (int)img.Length);
            return Convert.ToBase64String(imageArray);
        }

        public void Insert(Stream img)
        {
            using (BinaryReader pgReader = new BinaryReader(new BufferedStream(img)))
            {
                byte[] ImgByteA = pgReader.ReadBytes(Convert.ToInt32(img.Length));
                Dictionary<string, object> fields = new Dictionary<string, object>() { { "img", ImgByteA } };
                base.Insert(fields);
            }
        }

        /// <summary>
        /// Получить словарь полей таблицы для отображения в Grid. 
        /// Ключ - имя поля класса; Значение - имя/title для отображения.
        /// </summary>
        public override List<TableRowField> ViewTableFields()
        {
            return new List<TableRowField>()
            {
                new TableRowField("id", "Id", TableRowFieldType.Text, null, false, false),
                new TableRowField("img", "Изображение", TableRowFieldType.Image),
            };
        }
    }
}
