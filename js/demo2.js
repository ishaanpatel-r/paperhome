(function(){

	var button = document.getElementById('cn-button'),
    wrapper = document.getElementById('cn-wrapper');

    //open and close menu when the button is clicked
	var open = false;
	button.addEventListener('click', handler, false);

	function handler(){
	  if(!open){
	      this.innerHTML = "<i class='fa fa-minus'></i>";
	    classie.add(wrapper, 'opened-nav');
	  }
	  else{
	      this.innerHTML = "<i class='fa fa-plus'></i>";
		classie.remove(wrapper, 'opened-nav');
	  }
	  open = !open;
	}
	function closeWrapper(){
		classie.remove(wrapper, 'opened-nav');
	}

})();
