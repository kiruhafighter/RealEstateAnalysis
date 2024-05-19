import { configureStore } from '@reduxjs/toolkit';
import cart from './slices/cartSlice';
import form from './slices/formSlice';
import books from './slices/booksSlice';
import order from './slices/orderSlice';
import user from './slices/userSlice';

export const store = configureStore({
  reducer: {
    cart,
    form,
    books,
    order,
    user,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;