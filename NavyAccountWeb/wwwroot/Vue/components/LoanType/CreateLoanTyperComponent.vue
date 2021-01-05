<template>
    	<!-- WRAPPER -->
    <div>
        <div v-if="errors" class="has-error"> {{ [errors] }}</div>
        <div v-if="responseMessage" class="has-error"> {{ responseMessage }}</div>
        <form @submit="checkForm" action="/LoanType/CreateLoanTyper" method="post">
            <div class="card-body">
                <div class="row">
                   
                    <div class="col-12 col-xl-6">
                    <div class="form-group">
                        <label class="form-label">Loan Type</label>

                        <select class="form-control" v-model="postBody.LoanType" name="loanType" required>
                            <option v-for="loantype in loantypeList" v-bind:value="loantype.code" v-bind:key="loantype.code"> {{ loantype.description }} </option>
                        </select>

                    </div>
                </div>
                   
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">Interest</label>
                            <input class="form-control" name="interestrate" v-model="postBody.Interestrate" placeholder="" />
                        </div>
                    </div>
                   <div class="col-12 col-xl-6">
                                    <div class="form-group">
                                        <label class="form-label">Review Date</label>
                                        <vuejsDatepicker v-model="postBody.ReviewDate" :disabledDates="disabledDates" input-class="form-control" name="reviewDate"></vuejsDatepicker>
                                    </div>
                                </div>

                    
                    <div class="col-12 ">
                        <div class="btn-group mr-2 sw-btn-group-extra" v-if="canProcess" role="group"><button class="btn btn-submit btn-primary" v-on:click="checkForm" type="submit">{{submitorUpdate}}</button></div>
                    </div>
                </div>
                </div>
        </form>
    </div>
							
	<!-- END WRAPPER -->
</template>

<script>
import vuejsDatepicker from 'vuejs-datepicker'
    export default {
        components: {
            vuejsDatepicker
        },
    
    data() {
        return {
            errors: null,
            responseMessage:'',
            submitorUpdate : 'Submit',
            canProcess : true,
            loantypeList:null,

            postBody: {
                Interestrate:'',
                LoanType: '',
                ReviewDate:''

            }
    
        }
        },
     mounted () {
        axios
            axios
            .get('/api/LoanType/getAllLoanDesc')
            .then(response => (this.loantypeList = response.data))
       
       
     },
    watch:{
         '$store.state.objectToUpdate':function (newVal, oldVal) {
         this.postBody.LoanType = this.$store.state.objectToUpdate.loanType,
         this.postBody.ReviewDate = this.$store.state.objectToUpdate.reviewDate,
         this.postBody.Interestrate = this.$store.state.objectToUpdate.interestrate,


         this.submitorUpdate = 'Update';
               
        }
    },
    methods: {
        checkForm: function (e) {
            
         if (this.postBody.LoanType) {
              e.preventDefault();
              this.canProcess = false;
              this.postPost();
          }
          else{

          this.errors = [];
            this.errors.push('Code is Required');
          }
        },
        postPost() {
                if(this.submitorUpdate == 'Submit'){
                    axios.post(`/api/LoanType/createLoanTyper`, this.postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;
                            this.canProcess = true;
                            if (response.data.responseCode == '200') {
    
                                this.postBody.LoanType = ''; this.postBody.ReviewDate = '';
                                this.postBody.Interestrate = '';

                                  this.$store.state.objectToUpdate = "create";
                                
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                }
                if(this.submitorUpdate == 'Update'){
                    axios.put(`/api/LoanType/updateLoanTyper`, this.postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;this.canProcess = true;
                            if(response.data.responseCode == '200'){
                                this.submitorUpdate = 'Submit'
                                 this.postBody.LoanType = ''; this.postBody.ReviewDate = '';
                                this.postBody.Interestrate = '';
                                  this.$store.state.objectToUpdate = "update";
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                }
            }
        },
        computed: {
            setter(){
                let objecttoedit = this.$store.state.objectToUpdate;
                if(objecttoedit.loanTypeID){
       
                    this.postBody.reviewDate = objecttoedit.reviewDate;
                    this.postBody.loanTypeID = objecttoedit.loanTypeID;
                    this.postBody.Interest = objecttoedit.Interest;

                  
                }
            }
        }
};
</script>