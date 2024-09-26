import axios from "axios";
import { useState, useEffect} from 'react';
import './ClubDetails.css';
import { useParams, Link } from "react-router-dom";
import Button from './Button'

function ClubDetails({}){
    const { clubId } = useParams();
    const [club, setClub] = useState({});

    useEffect(() => {
        axios.get("https://localhost:7056/api/Club" + `/${clubId}`)
          .then(response => { 
            setClub(response.data); 
          })
          .catch(error => {
            console.error("Error fetching club: ", error);
          })
      }, [clubId]);

      

    return (
        <div className="clubDetails">
            <h2>Club Details</h2>
            <p><strong>Name:</strong> {club.name}</p>
            <p><strong>Sport:</strong> {club.sport}</p>
            <p><strong>Date of Establishment:</strong> {club.dateOfEstablishment}</p>
            <p><strong>Number of Members:</strong> {club.numberOfMembers}</p>
            <p><strong>President ID:</strong> {club.clubPresidentId}</p>

            {club.clubPresident && (
                <>
                    <p><strong>First Name:</strong> {club.clubPresident.firstName}</p>
                    <p><strong>Last Name:</strong> {club.clubPresident.lastName}</p>
                </>
            )}
            <Link to={"/"}><Button className="backButton" text="Back"/></Link>
        </div>
    );
}

export default ClubDetails;