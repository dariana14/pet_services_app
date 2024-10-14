import { IPrice } from "@/domain/IPrice";
import { BaseEntityService } from "./BaseEntityService";
import { IJWTResponse } from "@/dto/IJWTResponse";

export class PriceService extends BaseEntityService<IPrice> {
    constructor(setJwtResponse: ((data: IJWTResponse | null) => void)) {
        super('v1/Prices', setJwtResponse);
    }
}