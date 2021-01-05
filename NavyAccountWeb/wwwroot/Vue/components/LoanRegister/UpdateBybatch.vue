<template>
    	<!-- WRAPPER -->

      
        <div class="card-body">
            <div class="row">
                <div v-if="errors" class="has-error"> {{ [errors] }}</div>
                <div v-if="responseMessage" class="has-error"> {{ responseMessage }}</div>
                <div class="col-12 col-xl-6">
                    <div class="form-group">
                        <label class="form-label">Loan Type</label>

                        <select class="form-control" v-model="postBody.LoanTypeID" name="loanTypeID" required>
                            <option v-for="loantype in loantypeList" v-bind:value="loantype.id" v-bind:key="loantype.id"> {{ loantype.description }} </option>
                        </select>

                    </div>
                </div>
                <div class="col-12 col-xl-6">
                    <label class="form-label">Status </label>
                    <select class="form-control" v-model="postBody.status" name="status">
                        <option v-for="st in loanStatusList" v-bind:value="st.id" v-bind:key="st.id"> {{ st.description }} </option>
                    </select>
                </div>
                <div class="col-12 col-xl-6">
                    <div class="form-group">
                        <label class="form-label">Batch</label>

                        <select class="form-control" v-model="postBody.batchNo" name="batchNo" required>
                            <option v-for="batch in loanBatchList" v-bind:value="batch.batchNo" v-bind:key="batch.id"> {{ batch.batchNo }} </option>
                        </select>

                    </div>
                </div>
                <div class="col-4 ">
                    <div class="btn-group mr-2 sw-btn-group-extra" role="group"><button class="btn btn-submit btn-primary" v-on:click="checkForm" type="button">{{submitorUpdate}}</button></div>
                </div>
            </div>
            
        </div>

  

        <!-- END WRAPPER -->
</template>

<script>
export default { 
    props:['fundcodeid','statusdef','isapplication'],
            data() {
                return {
                    submitorUpdate:'Update',
                    loanregisterList: null,
                    loanStatusList:null,
                    loantypeList:null,
                    loanBatchList:null,
                    postBody: {
                        LoanTypeID: 0,
                        status:0,
                        batchNo: ''
                     
                     }
                };
            },
        
        
          watch:{
         '$store.state.objectToUpdate':function (newVal, oldVal) {
           
        }
    },
        mounted() {
          
        axios
            .get('/api/LoanType/getAllLoanDesc')
            .then(response => (this.loantypeList = response.data))
   

        axios
            .get('/api/LoanStatus/getAllLoanStatus')
                .then(response => (this.loanStatusList = response.data))

        axios
            .get('/api/LoanRegister/getAllBatch')
                .then(response => (this.loanBatchList = response.data))

        },
        methods: {
            
          checkForm: function (e) {
             this.postPost();
         if (this.postBody.status && this.postBody.id > 0) {
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
               alert(this.postBody.batchNo);
                    axios.put(`/api/LoanRegister/updateLoanRegisterbybatch`, this.postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;this.canProcess = true;
                            if(response.data.responseCode == '200'){
                                 this.submitorUpdate = 'Submit'
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                
            }
    }
    
};
</script>