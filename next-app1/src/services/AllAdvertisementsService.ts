import { IAdvertisement } from "@/domain/IAdvertisement";
import { BaseEntityService } from "./BaseEntityService";
import { IJWTResponse } from "@/dto/IJWTResponse";

export class AllAdvertisementService extends BaseEntityService<IAdvertisement> {
    constructor(setJwtResponse: ((data: IJWTResponse | null) => void)) {
        super('v1/AllAdvertisements', setJwtResponse);
    }

    async getAll(): Promise<IAdvertisement[] | undefined> {

        try {
            const response = await this.axios.get<IAdvertisement[]>('', {}
            );

            console.log('response', response);
            if (response.status === 200) {
                return response.data;
            }
            return undefined;

        } catch (e) {
            console.log('error: ', (e as Error).message);

            }

            return undefined;
    }

    async find(jwtData?: IJWTResponse, id?: string): Promise<IAdvertisement | undefined> {

        try {
            const response = await this.axios.get<IAdvertisement>('/' + id,
                {}
            );

            console.log('response', response);
            if (response.status === 200) {
                return response.data;
            }
            return undefined;

        } catch (e) {
            console.log('error: ', (e as Error).message);

            return undefined;
        }
    }
}