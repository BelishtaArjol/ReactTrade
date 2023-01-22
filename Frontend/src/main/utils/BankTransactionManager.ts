import axios from 'axios';

class BankTransactionManager
{

    static async getAllTransactions(): Promise<any>
    {
        const response = axios.get('banktransaction/get-all');
        return response;
    }

    static async getTransaction(id: number): Promise<any>
    {
        const response = axios.get(`banktransaction${id}`);
        return response;
    }

    static async addTransaction(transaction: Object): Promise<any>
    {
        const response = axios.post('banktransaction', transaction);
        return response;
    }

    static async deleteBank(id: number): Promise<any>
    {
        const response = axios.delete(`banktransaction/${id}`);
        return response;
    }

}

export default BankTransactionManager;
