"use client"

import { IAdvertisement } from "@/domain/IAdvertisement";
import { IServicePetCategory } from "@/domain/IServisePetCategory";
import { AllAdvertisementService } from "@/services/AllAdvertisementsService";
import { ServicePetCategoryService } from "@/services/ServicePetCategoryService";
import { AppContext } from "@/state/AppContext";
import { useContext, useEffect, useState } from "react";

export default function Advertisement({params} : {params : {id: string}}) {

    const { userInfo, setUserInfo } = useContext(AppContext)!;

    const allAdvertisementService = new AllAdvertisementService(setUserInfo);
    const servicePetCategoryService = new ServicePetCategoryService(setUserInfo);
    let advertisementId = params.id;

    const [advertisement, setAdvertisement] = useState<IAdvertisement>();
    const [servicePetCategories, setServicePetCategories] = useState([] as IServicePetCategory[]);

    useEffect(() => {
        allAdvertisementService.find(userInfo!, advertisementId).then(
            response => {
                if (response) {
                    setAdvertisement(response);
                }
            }
        );
    }, []);

    useEffect(() => {
        if (advertisement) {
            servicePetCategoryService.getAllByServiceId(advertisement?.serviceId!).then(
                response => {
                    if (response) {
                        setServicePetCategories(response);
                    }
                }
            );
        }

    }, [advertisement]);


    if (servicePetCategories === undefined || advertisement === undefined) return (<></>)

    return (
        <>
        <div className="text-center">
            <h2>Advertisement</h2>
            <h3>{advertisement?.title}</h3>
        </div>

        <hr />
        <dl className="row">
            <dt className = "col-sm-2">
                Description
            </dt>
            <dd className = "col-sm-10">
                {advertisement?.description}
            </dd>
            <dt className = "col-sm-2">
                Category
            </dt>
            <dd className = "col-sm-10">
                {advertisement?.categoryName}
            </dd>
            <dt className = "col-sm-2">
                Price
            </dt>
            <dd className = "col-sm-10">
                {advertisement?.priceValue}
            </dd>
            <dt className = "col-sm-2">
                Location
            </dt>
            <dd className = "col-sm-10">
                {advertisement?.city}
            </dd>
            <dt className = "col-sm-2">
                Status
            </dt>
            <dd className = "col-sm-10">
                {advertisement?.statusName === 1 ? "active" : "paused"}
            </dd>
        </dl>

        <div className="row">
            <h6>Pet categories</h6>
            {servicePetCategories.map(category =>
                <div className="column">
                    {category.petCategoryName} 
                </div>
            )}
        </div>
        </>
    );
}