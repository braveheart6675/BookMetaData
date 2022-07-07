namespace BookMetaDataApiDomain.DomainModel
{
    public class Book
    {
        #region Public Properties
        public string id { get; set; }
        public string title { get; set; }
        public string sourceBookId { get; set; }
        public string canonicalId { get; set; }
        public string subtitle { get; set; }
        public string description { get; set; }
        #endregion

        #region Equal and GetHashCode
        public override bool Equals(object? obj)
        {
            return obj is Book book &&
                   id == book.id &&
                   title == book.title &&
                   sourceBookId == book.sourceBookId &&
                   canonicalId == book.canonicalId &&
                   subtitle == book.subtitle &&
                   description == book.description;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, title, sourceBookId, canonicalId, subtitle, description);
        }
        #endregion

    }
}
