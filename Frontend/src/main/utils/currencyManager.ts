import axios from 'axios';

class CurrencyManager 
{

    static async getAllCurrencies(): Promise<any>
    {
        const response = axios.get('currency/get-all');
        return response;
    }

    static async getUniqueCurrency(id: number): Promise<any>
    {
        const response = axios.get(`currency/${id}`);
        return response;
    }

    static async addCurrency(product: Object): Promise<any>
    {
        const response = axios.post('currency', product);
        return response;
    }

    static async deleteCurrency(id: number): Promise<any>
    {
        const response = axios.delete(`currency/${id}`);
        return response;
    }

    static async editCurrency(id: number, product: Object): Promise<any>
    {
        const response = axios.put(`currency/${id}`, product);
        return response;
    }
}

export default CurrencyManager;
