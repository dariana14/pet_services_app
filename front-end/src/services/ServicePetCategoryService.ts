import { BaseEntityService } from "./BaseEntityService";
import { IJWTResponse } from "@/dto/IJWTResponse";
import { IServicePetCategory } from "@/domain/IServisePetCategory";

export class ServicePetCategoryService extends BaseEntityService<IServicePetCategory> {
    constructor(setJwtResponse: ((data: IJWTResponse | null) => void)) {
        super('v1/ServicePetCategories', setJwtResponse);
    }

    async getAllByServiceId(serviceId: string | undefined): Promise<IServicePetCategory[] | undefined> {

        try {
            const response = await this.axios.get<IServicePetCategory[]>('',
                {
                    params: {
                        "serviceId": serviceId
                    }
                }
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