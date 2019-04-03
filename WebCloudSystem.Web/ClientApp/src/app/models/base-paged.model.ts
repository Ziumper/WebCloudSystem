export class BasePagedModel<T> {
    public entities: Array<T>;
    public page: number;
    public count: number;
    public size: number;
}
