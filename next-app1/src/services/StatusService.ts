import { IStatus } from "@/domain/IStatus";
import { BaseEntityService } from "./BaseEntityService";
import { IJWTResponse } from "@/dto/IJWTResponse";

export class StatusService extends BaseEntityService<IStatus> {
    constructor(setJwtResponse: ((data: IJWTResponse | null) => void)) {
        super('v1/Statuses', setJwtResponse);
    }

    async getStatusId(name: number) {
        try {
            const response = await this.axios.get<IStatus[]>('');

            if (response.status === 200) {
                return response.data.filter(status => status.statusName == name)[0].id;
            }
            return undefined;

        } catch (e) {
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }

}