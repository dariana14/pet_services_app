"use client"

import { IAdvertisement } from "@/domain/IAdvertisement";
import { AdvertisementService } from "@/services/AdvertisementService";
import { AppContext } from "@/state/AppContext";
import Link from "next/link";
import { useContext, useEffect, useState } from "react";
import { useRouter } from "next/navigation";



export default function UserAdvertisements() {
    const [isLoading, setIsLoading] = useState(true);
    const [advertisements, setAdvertisements] = useState<IAdvertisement[]>([]);
    const { userInfo, setUserInfo } = useContext(AppContext)!;

    const advertisementService = new AdvertisementService(setUserInfo!);
    const router = useRouter();



    const loadData = async () => {
        const response = await advertisementService.getAll(userInfo!)
        if (response) {
            setAdvertisements(response);
        }
        setIsLoading(false);
    };

    useEffect(() => { 
        loadData();}, []);


    const onDelete = async (advertisementId: string) => {
        let response= await advertisementService.delete(userInfo!, advertisementId);
        router.push("/advertisements");
    }

    const drawAdvertisement = (advertisement: IAdvertisement) => {
        return (
            <li className="list-group-item">
                <span className={`badge status-badge ${
                  advertisement.statusName === 1 ? 'bg-success' : 'bg-warning'
                }`} >
                    {advertisement.statusName === 1 ? "Active" : "Paused"}
                </span> <br />
                <h5 className="mb-1">{advertisement.title}</h5>
                <p className="mb-1">{advertisement.description}</p>
                <br />
                <small className="text-muted">
                    City: {advertisement.city} | Price: {advertisement.priceValue} € | Category: {advertisement.categoryName}
                </small>
                <br />
                <Link href={"/advertisement/" + advertisement.id}><button className="btn btn-primary">View</button></Link>
                <span> </span>
                <Link href={"/editAdvertisement/" + advertisement.id}><button className="btn btn-primary">Edit</button></Link>
                <span className="float-end">
                 <button onClick={(a) => onDelete(advertisement.id)} className="btn btn-secondary">Delete</button>
                </span>
            </li>
        );
    }

    if (userInfo == null) router.push("/Identity/login");

    if (isLoading) return (<h1>Advertisements - LOADING</h1>);

    if (advertisements === undefined) return (<></>);


    return (
        <>
            <h1>Your Advertisements</h1>

            <p>
                <Link href={"/createAdvertisement"}>Create New</Link>
            </p>

            <ul className="list-group">
                {advertisements.map(advertisement => drawAdvertisement(advertisement))}
            </ul>

        </>
    );
}