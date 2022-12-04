namespace WebApplication2.ViewModel
{
    public class AddressViewModel
    {

        public class Rootobject
        {
            public Class1[] Property1 { get; set; }
        }

        public class Class1
        {
            public string value { get; set; }
            public int length { get; set; }
            public Addresspart[] addressParts { get; set; }
        }

        public class Addresspart
        {
            public string objectGuid { get; set; }
            public string name { get; set; }
            public string typeName { get; set; }
            public string fullTypeName { get; set; }
            public int level { get; set; }
            public string kladr { get; set; }
            public string okato { get; set; }
            public string oktmo { get; set; }
            public string postIndex { get; set; }
            public bool isActive { get; set; }
            public object sublevels { get; set; }
        }

    }
}
