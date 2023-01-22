
export default interface IProduct
{
	id?: string;
	name: string;
	shortDescription: string;
	longDescription: string;
	base64: string;
	categoryId?: string;
	price: number;
	event: React.FormEvent;
}
