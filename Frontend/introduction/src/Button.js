import './Button.css'

function Button({ text, onClick, className, type = 'submit' }){
    return (
        <button className={className} type={type} onClick={onClick}>
            {text}
        </button>
    );
}


export default Button;