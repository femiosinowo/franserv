using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.Xml;


namespace Ektron.Cms.DxH.Content
{

    [XmlRoot("root")]
    public class EntityItem : IXmlSerializable
    {

        public EntityItem() { Properties = new EntityPropertyList(); }

        public long Id { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public EntityPropertyList Properties { get; set; }


        #region IXmlSerializable Members

        System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
        {
            throw new NotImplementedException();
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            Name = reader.GetAttribute("Name");
            LanguageId = int.Parse(reader.GetAttribute("LanguageId"));
            Id = long.Parse(reader.GetAttribute("Id"));

            while (reader.Read())
            {
                if (reader.Name == "property")
                {
                    EntityItemProperty property = new EntityItemProperty();
                    ((IXmlSerializable)property).ReadXml(reader);
                    Properties.Add(property);
                }
            }

            //reader.ReadEndElement();
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("Id", Id.ToString());
            writer.WriteAttributeString("LanguageId", LanguageId.ToString());
            writer.WriteAttributeString("Name", Name);

            foreach (EntityItemProperty property in Properties)
            {
                writer.WriteStartElement("property");
                ((IXmlSerializable)property).WriteXml(writer);
                writer.WriteEndElement();
            }
        }

        #endregion
    }

    public class EntityItemProperty : IXmlSerializable
    {
        [XmlAttribute]
        public long Id { get; set; }
        
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string DisplayName { get; set; }

        public Object Value { get; set; }
        
        [XmlAttribute]
        public ItemDataType Type { get; set; }

        #region IXmlSerializable Members

        System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            //reader.MoveToContent();
            Name = reader.GetAttribute("Name");
            //Type = Type. reader.GetAttribute("Type");
            Id = long.Parse(reader.GetAttribute("Id"));

            reader.ReadStartElement();
            Value = reader.ReadContentAsObject();
            //reader.ReadEndElement();
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("Id", Id.ToString());
            writer.WriteAttributeString("Type", Type.ToString());
            writer.WriteAttributeString("Name", Name);
            writer.WriteValue(Value.ToString());

        }

        #endregion
    }

    public enum ItemDataType
    {
        Object = 0,
        String,
        Integer,
        DateTime,
        SelectList
    }

    public class EntityPropertyList : System.Collections.ObjectModel.KeyedCollection<string, EntityItemProperty>, IList<EntityItemProperty>
    {

        public EntityPropertyList() : base() { }

        protected override string GetKeyForItem(EntityItemProperty item)
        {
            return item.Name;
        }

        #region IList<EntityItemProperty> Members

        int IList<EntityItemProperty>.IndexOf(EntityItemProperty item)
        {
            throw new NotImplementedException();
        }

        void IList<EntityItemProperty>.Insert(int index, EntityItemProperty item)
        {
            throw new NotImplementedException();
        }

        void IList<EntityItemProperty>.RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        EntityItemProperty IList<EntityItemProperty>.this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}