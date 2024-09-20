import { ILocation } from "@/domain/ILocation";
import { BaseEntityService } from "./BaseEntityService";
import { IJWTResponse } from "@/dto/IJWTResponse";

export class LocationService extends BaseEntityService<ILocation> {
    constructor(setJwtResponse: ((data: IJWTResponse | null) => void)) {
        super('v1/Locations', setJwtResponse);
    }
}
