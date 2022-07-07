namespace BookMetaData.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BookMetaData")]
    public class BookMetaData
    {
        #region Public Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }
        public string title { get; set; }
        public string sourceBookId { get; set; }
        public string canonicalId { get; set; }
        public string subtitle { get; set; }
        public string description { get; set; }
        #endregion


        #region Equals GetHashCode
        public override bool Equals(object? obj)
        {
            return obj is BookMetaData data &&
                   id == data.id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id);
        }
        #endregion
    }
}
