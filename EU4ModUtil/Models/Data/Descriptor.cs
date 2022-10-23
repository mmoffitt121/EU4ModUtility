using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace EU4ModUtil.Models.Data
{
    internal class Descriptor : TXTFileObject
    {
        public string version;
        public List<string> replacePaths;
        public List<string> tags;
        public string name;
        public string supportedVersion;
        public string picture;
        public BitmapImage bitmap;

        public Descriptor (TXTFileObject rawData)
        {
            if (rawData == null || rawData.values == null)
            {
                throw new ArgumentNullException(nameof(rawData));
            }

            replacePaths = new List<string>();
            List<AttributeValueObject> values = new List<AttributeValueObject>();

            foreach (AttributeValueObject item in rawData.values)
            {
                if (item.attribute == null)
                {
                    continue;
                }
                switch (item.attribute.ToLower())
                {
                    case "version":
                        version = item.value.attribute;
                        break;
                    case "replace_path":
                        replacePaths.Add(item.value.attribute);
                        break;
                    case "tags":
                        tags = LoadTags(item);
                        break;
                    case "name":
                        name = item.value.attribute;
                        break;
                    case "supported_version":
                        supportedVersion = item.value.attribute;
                        break;
                    case "picture":
                        picture = item.value.attribute;
                        break;
                    default:
                        values.Add(item);
                        break;
                }
            }

            this.values = values.ToArray();
        }

        public List<string> LoadTags(AttributeValueObject item)
        {
            List<string> result = new List<string>();

            foreach (AttributeValueObject value in item.values)
            {
                result.Add(value.attribute);
            }

            return result;
        }
    }
}
