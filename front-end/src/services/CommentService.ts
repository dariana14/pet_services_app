import { IComment } from "@/domain/IComment";
import { BaseEntityService } from "./BaseEntityService";
import { IJWTResponse } from "@/dto/IJWTResponse";

export class CommentService extends BaseEntityService<IComment> {
    constructor(setJwtResponse: ((data: IJWTResponse | null) => void)) {
        super('v1/Comments', setJwtResponse);
    }
}