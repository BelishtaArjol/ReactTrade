import IBuyProduct from "./IBuyProduct";

export default interface IProductPayload
{
    cartItem : IBuyProduct[],
    total: number
}