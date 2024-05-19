import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { Book, BookSliceState, Status } from "./types";
import axios from "axios";



export const fetchBooks = createAsyncThunk<Book[]>(
  'books/fetchBooksStatus',
  async () => {
    const {data} = await axios.get<Book[]>(
      `https://640b4d0865d3a01f9816ff9d.mockapi.io/items`
    );
    return data;
  }
);

const initialState:BookSliceState = {
  items: [],
  status: Status.LOADING,
};

const booksSlice = createSlice({
  name: 'books',
  initialState,
  reducers: {
    setItem(state, action){
      state.items = action.payload;
    },
  },
  extraReducers:(builder) => {
    builder.addCase(fetchBooks.pending, (state) => {
      state.status = Status.LOADING;
      state.items = [];
     })
     builder.addCase(fetchBooks.fulfilled, (state, action) => {
      state.items = action.payload;
      state.status = Status.SUCCESS;
     })
     builder.addCase(fetchBooks.rejected, (state) => {
      state.status = Status.ERROR;
      state.items = [];
     })
    },
});

export const {setItem} = booksSlice.actions;
export default booksSlice.reducer;