


function PresidentRow({ president }) {
    return (
      <tr key={president.id}>
        <td>{president.firstName}</td>
        <td>{president.lastName}</td>
      </tr>
    );
  }


export default PresidentRow;