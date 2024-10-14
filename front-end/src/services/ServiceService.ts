import { IService } from "@/domain/IService";
import { BaseEntityService } from "./BaseEntityService";
import { IJWTResponse } from "@/dto/IJWTResponse";

export class ServiceService extends BaseEntityService<IService> {
    constructor(setJwtResponse: ((data: IJWTResponse | null) => void)) {
        super('v1/Services', setJwtResponse);
    }
}