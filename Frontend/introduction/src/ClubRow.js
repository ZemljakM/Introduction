import Button from './Button';


function ClubRow({ club, deleteClub, updateClub, selectClub }) {
    return (
      <tr key={club.id} onClick={() => selectClub(club.id)} style={{ cursor: 'pointer' }}>
        <td>{club.name}</td>
        <td>{club.sport}</td>
        <td>{club.dateOfEstablishment}</td>
        <td>{club.numberOfMembers}</td>
        <td>{club.clubPresident.firstName + " " + club.clubPresident.lastName}</td>
        <td>
            <Button className="updateClub" text="Update club" onClick={() => updateClub(club.id)} />
            <Button className="deleteClub" text="Delete club" onClick={() => deleteClub(club.id)} />
        </td>
      </tr>
    );
  }


export default ClubRow;