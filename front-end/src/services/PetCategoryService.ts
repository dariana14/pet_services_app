import { BaseEntityService } from "./BaseEntityService";
import { IJWTResponse } from "@/dto/IJWTResponse";
import { IPetCategory } from "@/domain/IPetCategory";

export class PetCategoryService extends BaseEntityService<IPetCategory> {
    constructor(setJwtResponse: ((data: IJWTResponse | null) => void)) {
        super('v1/PetCategories', setJwtResponse);
    }
}