import { createSlice } from "@reduxjs/toolkit";
import { getUserFromLS } from "../../utils/getFromLS";
import { UserSliceState } from "./types";

const {isLogin, email} = getUserFromLS();

const initialState:UserSliceState = {
  email,
  token: null,
  id:null,
  isLogin,
};

const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    setUser(state, action){
      state.email = action.payload.email;
      state.token = action.payload.token;
      state.id = action.payload.id;
      state.isLogin = true;
    },
    removeUser(state){
      state.email = null;
      state.token = null;
      state.id = null;
      state.isLogin = false;
    },
  },
},
);

export const {setUser, removeUser} = userSlice.actions;
export default userSlice.reducer;