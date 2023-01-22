import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import IBuyProduct  from "../../../interfaces/IBuyProduct";
import IProductPayload  from "../../../interfaces/IProductPayload";

const initialState: IProductPayload = 
{
    cartItem: [],
    total: 0
}

const cardStore = createSlice(
{
    name: 'card',
    initialState: initialState,
    reducers: 
    {
        buyProduct(state, action: PayloadAction<IBuyProduct>) 
        {
            state.cartItem.push(action.payload);
            state.total = state.total + action.payload.quantity * action.payload.price;
        },
        removeItem(state, action: PayloadAction<IBuyProduct>)
        {
            const { id } = action.payload;
            const index = state.cartItem.findIndex((item) => item.id === id);
            state.total = state.total - action.payload.quantity * action.payload.price;
            state.cartItem.splice(index, 1);

        },
        incrementItem(state, action: PayloadAction<IBuyProduct>)
        {
            const {quantity, id} = action.payload;
            const index = state.cartItem.findIndex((item) => item.id === id );
            
            return { 
                ...state, 
                cartItem: state.cartItem.map(
                    (cart, i) => i === index ? {...cart, quantity: quantity+1} : cart                
                ),
                total: state.total + action.payload.price
            }
        },
        updateItem(state, action: PayloadAction<IBuyProduct>)
        {
            const {quantity, id} = action.payload;
            const index = state.cartItem.findIndex((item) => item.id === id);
            return { 
                ...state, 
                cartItem: state.cartItem.map(
                    (cart, i) => i === index ? {...cart, quantity: quantity} : cart                
                ),
                total: action.payload.quantity * action.payload.price

            }

        },
        decrementItem(state, action: PayloadAction<IBuyProduct>)
        {
            const {quantity, id, price} = action.payload;
            const index = state.cartItem.findIndex((item) => item.id === id);
            return { 
                ...state, 
                cartItem: state.cartItem.map(
                    (cart, i) => (i === index && quantity > 1) ? {...cart, quantity: quantity-1} : cart
                                    
                ),
                total: (quantity === 1) ? state.total : state.total - action.payload.price

            }
        },
        clearCard(state)
        {
            return {
                ...state,
                cartItem: [],
                total: 0
            }
        }
    },
});

export default cardStore;

export const { buyProduct,removeItem,incrementItem,decrementItem,updateItem, clearCard } = cardStore.actions;
