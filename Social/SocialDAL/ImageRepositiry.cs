using System;
using System.IO;
using System.Collections.Generic;
using SOV.Common;
using Npgsql;

namespace SOV.Social
{
    public class ImageRepository : BaseRepository<Image>
    {
        public ImageRepository(Common.ADbNpgsql db) : base(db, "social.image") {}

        protected override object ParseData(NpgsqlDataReader reader)
        {
            return new Image() {Id = (int) reader["id"], Img = reader["img"] as byte[]};
        }

        public static string ConvertImageToBase64(Stream img)
        {
            byte[] imageArray;

            imageArray = new byte[img.Length];
            img.Seek(0, System.IO.SeekOrigin.Begin);
            img.Read(imageArray, 0, (int)img.Length);
            return Convert.ToBase64String(imageArray);
        }

        public int Insert(Stream img)
        {
            using (BinaryReader pgReader = new BinaryReader(new BufferedStream(img)))
            {
                byte[] ImgByteA = pgReader.ReadBytes(Convert.ToInt32(img.Length));
                Dictionary<string, object> fields = new Dictionary<string, object>() { { "img", ImgByteA } };
                return InsertWithReturn(fields);
            }
        }

        public List<int> Insert(List<byte[]> bytes)
        {
            List<Dictionary<string, object>> fieldsList = new List<Dictionary<string, object>>();
            foreach (var img in bytes)
                fieldsList.Add(new Dictionary<string, object>() { { "img", img } });
            return InsertWithFullReturn(fieldsList);
        }
    }
}
