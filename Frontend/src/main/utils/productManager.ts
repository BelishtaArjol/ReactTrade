import axios from 'axios';

class ProductManager 
{

    static async getAllProducts(): Promise<any>
    {
        const response = axios.get('product/get-all');
        return response;
    }

    static async getUniqueProduct(id: number): Promise<any>
    {
        const response = axios.get(`product/get-all/${id}`);
        return response;
    }

    static async addProduct(product: Object): Promise<any>
    {
        const response = axios.post('product', product);
        return response;
    }

    static async deleteProduct(id: number): Promise<any>
    {
        const response = axios.delete(`product/get-all/${id}`);
        return response;
    }

    static async editProduct(id: number): Promise<any>
    {
        const response = axios.put(`product/get-all/${id}`);
        return response;
    }
}

export default ProductManager;
