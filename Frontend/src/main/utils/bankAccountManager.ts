import axios from 'axios';

class BankAccountManager
{
    static getAllCurrencies() {
        throw new Error("Method not implemented.");
    }

    static async getAllBanks(): Promise<any>
    {
        const response = axios.get('bankaccount/get-all');
        return response;
    }

    static async getUniqueBank(id: number): Promise<any>
    {
        const response = axios.get(`bankaccount/${id}`);
        return response;
    }

    static async addBank(bank: Object): Promise<any>
    {
        const response = axios.post('bankaccount', bank);
        return response;
    }

    static async deleteBank(id: number): Promise<any>
    {
        const response = axios.delete(`bankaccount/${id}`);
        return response;
    }

    static async editBank(id: number, bank: Object): Promise<any>
    {
        const response = axios.put(`bankaccount/${id}`, bank);
        return response;
    }
}

export default BankAccountManager;
