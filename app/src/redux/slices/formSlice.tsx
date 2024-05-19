import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { getFormFromLS } from "../../utils/getFromLS";
import { FormSliceState, ValuesItems } from "./types";



const {values} = getFormFromLS();

const initialState:FormSliceState = {
  values,
};

const formSlice = createSlice({
  name: 'form',
  initialState,
  reducers: {
    submitForm(state, action: PayloadAction<ValuesItems>){
      const lastOrderNum = state.values[state.values.length - 1]?.orderNum ?? 0;
      const newOrderNum = lastOrderNum + 1;
      state.values.push({
        ...action.payload,
        orderNum: newOrderNum,
      });
      },
      updateOrder(state, action: PayloadAction<{ id: number, updatedValues: ValuesItems }>) {
        const { id, updatedValues } = action.payload;
        if (id !== -1) {
          state.values[id - 1] = updatedValues;
        }
      }
  },
},
);

export const {submitForm, updateOrder} = formSlice.actions;
export default formSlice.reducer;