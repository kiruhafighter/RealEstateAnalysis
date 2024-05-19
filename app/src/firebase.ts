import { initializeApp } from "firebase/app";
import { getAnalytics } from "firebase/analytics";

const firebaseConfig = {
  apiKey: "AIzaSyAyL-y1BaaVtr0kxTVt24m7rfGGIZ_vQeU",
  authDomain: "itp-labrary-auth.firebaseapp.com",
  databaseURL: "https://itp-labrary-auth-default-rtdb.firebaseio.com",
  projectId: "itp-labrary-auth",
  storageBucket: "itp-labrary-auth.appspot.com",
  messagingSenderId: "733568280150",
  appId: "1:733568280150:web:c1fe2785cf3fc53a1ffdf7",
  measurementId: "G-7QXTEWCLDF"
};

const app = initializeApp(firebaseConfig);
const analytics = getAnalytics(app);
