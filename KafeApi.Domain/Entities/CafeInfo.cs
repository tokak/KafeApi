namespace KafeApi.Domain.Entities;

public class CafeInfo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string WebsiteUrl { get; set; }
    public string InstagramUrl { get; set; }
    public string YoutubeUrl { get; set; }
    public string FacebookUrl { get; set; }
    public string TwitterUrl { get; set; }
    public string OpeningHours { get; set; }
    public string ImageUrl { get; set; }
    // One-to-Many ilişki: Bir cafe birçok yoruma sahip olabilir
    //public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
