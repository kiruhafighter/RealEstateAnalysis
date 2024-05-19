import { createSlice} from "@reduxjs/toolkit";
import { getOrderFromLS } from "../../utils/getFromLS";

type OrderItems = {
  totalAmount:number;
  totalPrice: number;
  orderNum: number;
  isCompleted: boolean;
}

interface OrderSliceState {
  orderInfo: OrderItems[];
  isCompleted: boolean;
}

const {orderInfo} = getOrderFromLS();

const initialState:OrderSliceState = {
  orderInfo,
  isCompleted: true,
};

const orderSlice = createSlice({
  name: 'order',
  initialState,
  reducers: {
    createOrder(state, action){
      const lastOrderNum = state.orderInfo[state.orderInfo.length - 1]?.orderNum ?? 0;
      const newOrderNum = lastOrderNum + 1;
      state.isCompleted = !state.isCompleted;
      state.orderInfo.push({
        ...action.payload,
        isCompleted: state.isCompleted,
        orderNum: newOrderNum,
      });
      },
  },
},
);

export const {createOrder} = orderSlice.actions;
export default orderSlice.reducer;