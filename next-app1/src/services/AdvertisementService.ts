import { IAdvertisement } from "@/domain/IAdvertisement";
import { BaseEntityService } from "./BaseEntityService";
import { IJWTResponse } from "@/dto/IJWTResponse";

export class AdvertisementService extends BaseEntityService<IAdvertisement> {
    constructor(setJwtResponse: ((data: IJWTResponse | null) => void)) {
        super('v1/Advertisements', setJwtResponse);
    }
}