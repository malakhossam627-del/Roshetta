using Rosetta.DAL.Entites.Order;
using CartEntity = Rosetta.DAL.Entites.Cart.Cart;

namespace Rosetta.DAL.Entites.Pharmacy
{
    public class Pharmacy
    {
        public int Id { get; set; }

        public ICollection<CartEntity> Carts { get; set; } = new List<CartEntity>();

        public ICollection<CustomerOrder> Orders { get; set; } = new List<CustomerOrder>();
    }
}