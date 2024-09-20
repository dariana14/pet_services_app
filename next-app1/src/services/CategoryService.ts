import { BaseEntityService } from "./BaseEntityService";
import { IJWTResponse } from "@/dto/IJWTResponse";
import { ICategory } from "@/domain/ICategory";

export class CategoryService extends BaseEntityService<ICategory> {
    constructor(setJwtResponse: ((data: IJWTResponse | null) => void)) {
        super('v1/Categories', setJwtResponse);
    }
}