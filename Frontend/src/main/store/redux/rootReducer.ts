import { combineReducers } from '@reduxjs/toolkit';
import userStore from '../stores/user/user.store';
import navigationStore from '../stores/navigation/navigation.store';
import cardStore from '../stores/card/cardStore';

const rootReducer = combineReducers({
  user: userStore.reducer,
  navigation: navigationStore.reducer,
  card: cardStore.reducer
});

export default rootReducer;
