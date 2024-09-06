namespace C4Admin.DataModel
{
    class C4Server
    {
        public string Id { get; private set; }
        public string Label { get; private set; }
        public string BaseUrl { get; private set; }
        public C4Server(string id, string label, string baseUrl)
        {
            Id = id;
            Label = label;
            BaseUrl = baseUrl;
        }
        public override string ToString()
        {
            return Label;
        }
    }
}
