namespace CafeReservation.WebAPI.Contracts
{
    public record AddItemToCartRequest(
        int MenuItemId,
        int Quantity);
}
