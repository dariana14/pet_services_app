"use client"

import { IAdvertisement } from "@/domain/IAdvertisement";
import { AllAdvertisementService } from "@/services/AllAdvertisementsService";
import { AppContext } from "@/state/AppContext";
import Link from "next/link";
import { useContext, useEffect, useState } from "react";


export default function Advertisements() {
    const [isLoading, setIsLoading] = useState(true);
    const [advertisements, setAdvertisements] = useState<IAdvertisement[]>([]);
    const { userInfo, setUserInfo } = useContext(AppContext)!;

    const allAdvertisementService = new AllAdvertisementService(setUserInfo!);


    const loadData = async () => {
        const response = await allAdvertisementService.getAll()
        if (response) {
            setAdvertisements(response.filter(a => a.statusName === 1));
        }
        setIsLoading(false);
    };

    useEffect(() => { 
        loadData();

    }, []);



    const drawAdvertisement = (advertisement: IAdvertisement) => {
        return (
            <li className="list-group-item">
                <h5 className="mb-1">{advertisement.title}</h5>
                <p className="mb-1">{advertisement.description}</p>
                <br />
                <small className="text-muted">
                    City: {advertisement.city} | Price: {advertisement.priceValue} € | Category: {advertisement.categoryName}
                </small>
                <br />
                {/* <small className="text-muted">Contact: {advertisement.userEmail}</small> */}
                <Link href={"/advertisement/" + advertisement.id}><button className="btn btn-primary">View</button></Link>
            </li>
        );
    }


    if (isLoading) return (<h1>Advertisements - LOADING</h1>);

    if (advertisements === undefined) return (<></>);

    return (
        <>
            <h3>Services</h3>

            <hr/>

            <ul className="list-group">
                {advertisements.map(advertisement => 
                    drawAdvertisement(advertisement))}
            </ul>

        </>
    );
}