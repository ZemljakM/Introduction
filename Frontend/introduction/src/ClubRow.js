import Button from './Button';


function ClubRow({ club, deleteClub, updateClub }) {
    return (
      <tr>
        <td>{club.name}</td>
        <td>{club.sport}</td>
        <td>{club.dateOfEstablishment}</td>
        <td>{club.members}</td>
        <td>{club.president}</td>
        <td>
            <Button className="updateClub" text="Update club" onClick={() => updateClub(club.id)} />
            <Button className="deleteClub" text="Delete club" onClick={() => deleteClub(club.id)} />
        </td>
      </tr>
    );
  }


export default ClubRow;