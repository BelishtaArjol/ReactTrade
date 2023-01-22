import AppNavigate from "./AppNavigate";
import PrivateRoute from "./private-route";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import TestPage from "../pages/Authentication/Login";
import DashboardPage from "../pages/Dashboard";
import Register from "../pages/Authentication/Register";
import Profile from "../main/components/Profile";
import LastTransactions from "../main/components/LastTransactions";
import BankAccounts from "../pages/bankAccount/BankAccounts";
import AddBankAccount from "../pages/bankAccount/AddBankAccount";
import EditBankAccount from "../pages/bankAccount/EditBankAccount";
import ProductToBuy from "../pages/Product/ProductToBuy";

const App = () => {
  return (
    <BrowserRouter>
      <AppNavigate />
      <Routes>
        <Route path="/" element={<DashboardPage />} />
        <Route
          path="/login"
          element={
            <PrivateRoute isPageLogin>
              <TestPage />
            </PrivateRoute>
          }
        />
        <Route path="/register" element={<Register />} />

        <Route path="/bankAccounts" element={<BankAccounts />} />
        <Route path="/AddBankAccount" element={<AddBankAccount />} />
        <Route path="/EditBankAccount/:id" element={<EditBankAccount />} />
        <Route path="/ProductToBuy" element={<ProductToBuy />} />
        <Route path="/Profile" element={<Profile />} />
        <Route path="/lastTrasactions" element={<LastTransactions />} />
      </Routes>
    </BrowserRouter>
  );
};

export default App;
