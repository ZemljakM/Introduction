import Button from './Button';
import { Link } from 'react-router-dom';
import { useState } from 'react';
import Modal from './Modal';


function ClubRow({ club, deleteClub}) {
  const [isModalOpen, setIsModalOpen] = useState(false);

  const handleDelete = () => {
    setIsModalOpen(true); 
  };

  const confirmDelete = () => {
    deleteClub(club.id); 
    setIsModalOpen(false); 
  };

  const closeModal = () => {
    setIsModalOpen(false);
  };


    return (
      <>
        <tr>
          <td>{club.name}</td>
          <td>{club.sport}</td>
          <td>{club.dateOfEstablishment}</td>
          <td>{club.numberOfMembers}</td>
          <td>{club.clubPresident.firstName + " " + club.clubPresident.lastName}</td>
          <td>
              <Link to={`/editClub/${club.id}`}><Button className="updateClub" text="Update club"/></Link>
              <Button className="deleteClub" text="Delete club" onClick={handleDelete} />
              <Link to={`/details/${club.id}`}><Button className="showClub" text="Details"/></Link>
          </td>
        </tr>
        <Modal isOpen={isModalOpen} onClose={closeModal} onConfirm={confirmDelete}/>
      </>
    );
  }


export default ClubRow;