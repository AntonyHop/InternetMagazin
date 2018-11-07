
namespace InternetMagazine.PL.DTO
{
    public class CategoryDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return this.Name;
        }

    }
}
