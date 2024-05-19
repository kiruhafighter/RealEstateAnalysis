import { createSlice } from "@reduxjs/toolkit";
import { calcTotalAmount, calcTotalPrice } from "../../utils/calcTotal";
 import { getCartFromLS } from "../../utils/getFromLS"; 
 import { CartSliceState } from "./types";


const {items, totalAmount, totalPrice} = getCartFromLS(); 

const initialState:CartSliceState = {
  totalAmount,
  totalPrice,
  items,
};

const cartSlice = createSlice({
  name: "cart",
  initialState,
  reducers: {
    addItem(state, action) {
      const findItem = state.items.find((obj) => obj.id === action.payload.id);
      if (findItem) {
        findItem.count++;
      } else {
        state.items.push({
          ...action.payload,
          count: 1,
        });
      }
      state.totalPrice = calcTotalPrice(state.items);
      state.totalAmount = calcTotalAmount(state.items);
    },
    removeItem(state, action) {
      const findItem = state.items.find((obj) => obj.id === action.payload);

      if (findItem.count > 1) {
        findItem.count--;
      } 

      state.totalPrice = calcTotalPrice(state.items);
      state.totalAmount = calcTotalAmount(state.items);
    },
    deleteItem(state, action){
      state.items = state.items.filter((obj) => obj.id !== action.payload);
      state.totalPrice = calcTotalPrice(state.items);
      state.totalAmount = calcTotalAmount(state.items); 
    },
    cleanItems(state) {
      state.items = [];
      state.totalAmount = 0;
      state.totalPrice = 0;
    },
  },
});

export const { addItem, removeItem, cleanItems, deleteItem } = cartSlice.actions;
export default cartSlice.reducer;
