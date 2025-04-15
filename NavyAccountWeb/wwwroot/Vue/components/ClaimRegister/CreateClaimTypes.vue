<template>
    <!-- WRAPPER -->
    <div>

        <div v-if="errors" class="alert alert-danger alert-dismissible" role="alert">
            <div class="alert-message">
                {{ [errors] }}
            </div>
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">�</span>
            </button>
        </div>
        <div v-if="responseMessage" class="alert alert-primary alert-dismissible" role="alert"> <div class="alert-message"> {{ responseMessage }} </div> </div>
        <div class="card-body">

            <div class="row">
                 <div class="col-12 col-xl-6" hidden>
                    <div class="form-group">
                        <label class="form-label">Fund type</label>
                          <input class="form-control" name="description" v-model="postBody.FundTypeID" readonly />

                    </div>
                </div>
                <div class="col-12 col-xl-6">
                    <div class="form-group">
                        <label class="form-label">Service Number</label>
                        <vuejsAutocomplete source="/api/PersonAPI/getAllPersonsByServiceNoLimited/"
                                           input-class="form-control"
                                           @selected="setValuePersonID"
                                           v-model="postBody.PersonID">
                        </vuejsAutocomplete>

                    </div>
                </div>
            </div>
        <div v-if="claimlist" class="card-body">
        <div v-show="wantshow2">
             <table id="datatables-buttons" class="table table-striped" style="width:100%">
                <thead>
                    <tr>
                        <th>Contribution Amount</th>
                        <th v-if="postBody.FundTypeID">Loan Amount</th>
                        <th>Amount Due</th>

                    </tr>
                </thead>
                <tbody>
                    <tr v-for="discharges in claimlist">
               
                        <td v-if="discharges.totalContribution">{{discharges.totalContribution}}</td>
                        <td v-if="discharges.amountReceived">{{ discharges.amountReceived}}</td>
                        <td v-if="discharges.amountDue">{{ discharges.amountDue }}</td>
                        <td v-if="discharges.amountPaid>=0"><button type="button" class="btn btn-submit btn-primary" @click="processRetrieve(discharges)">Add Beneficiary</button></td>
                    </tr>
                </tbody>
             

            </table>
        </div>     
        <div  v-show="wantshow">
            <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label class="form-label">Contribution</label>
                    <input class="form-control" name="description" v-model="postBody.totalContribution" readonly />
                </div>
            </div>

            <div class="col-md-4" v-if="postBody.Amount">
                <div class="form-group">
                    <label class="form-label">10% Of Amount</label>
                    <input class="form-control" name="description" v-model="postBody.amountPaid" readonly />
                </div>
            </div>

            <div class="col-md-4" v-if="postBody.amountReceived">
                <div class="form-group">
                    <label class="form-label">Outstanding Amount</label>
                    <input class="form-control" name="description" v-model="postBody.amountReceived" readonly />

                </div>
            </div>

            <div class="col-md-4">
                <div class="form-group">
                    <label class="form-label">Amount Due</label>
                    <input class="form-control" name="description" v-model="postBody.amountDue" readonly />

                </div>
            </div>
            </div>
            <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label class="form-label">Beneficiary</label>
                    <input class="form-control" name="description" v-model="postBody.Beneficiary" />

                </div>
            </div>
            <div class="col-sm-4">
                <label>Bank Name</label>
                <select class="form-control" v-model="postBody.bank" name="bank" required>
                    <option v-for="bank in bankList" v-bind:value="bank.id" v-bind:key="bank.id"> {{ bank.bankname }} </option>
                </select>
            </div>
            <div class="col-sm-4">
                <label>Account Number</label>
                <input v-model="postBody.acctno" class="form-control" @input="assertMaxChars()">
                <!-- <input class="form-control" name="acctno" v-model="postBody.acctno" @input="assertMaxChars() required /> -->
            </div>
            </div>

            <div class="row">
                <div class="btn-group mr-2 sw-btn-group-extra" v-if="canProcess" role="group">
                    <button class="btn btn-submit btn-primary" v-on:click="checkForm" type="submit">{{submitorUpdate}}</button>
                </div>
                <!-- <div class="col-4" v-show="wantshow3">
                <div class="form-group" >
                    <br />
                    <button class="btn btn-primary" v-on:click="generateReport" type="button">{{submitorUpdate}}</button>
                </div> -->
            </div>
            </div>

  

        </div>

            </div>

        </div>

    <!-- END WRAPPER -->
</template>

<script>
    import vuejsAutocomplete from 'vuejs-auto-complete'
export default {
     components: {

         vuejsAutocomplete
        },
    props:['fundtypeid'],
    data() {

        return {
        responseMessage:'',
            errors: null,
            searchData: '',
            submitorUpdate: 'submit',
            loantypeList: null,
            autoselectenabled: false,
            canProcess: true,
            claimlist:null,
            bankList:null,
            wantshow:false,
            wantshow2:true,
            wantshow3:false,
            claim:null,
            textlimit:10,
            acctno:"",
            maxLengthInCars: 10,
        postBody: {
            beneficiary:'',
            bank:0,
            PersonID:0,
            FundTypeID: 20,
            amountPaid: 0,
            amountReceived: 0,
            amountDue: 0,
            totalContribution: 0,
            acctno:''
  
        },
        };
        },
        mounted() {
            this.$store.state.objectToUpdate = null,
        axios
            .get('/api/FundType/getAllFundTypes')
            .then(response => (this.loantypeList = response.data))
              axios
                .get('/api/Bank/getAllBanks')
                .then(response => (this.bankList = response.data));
     },
     watch:{
        '$store.state.objectToUpdate':function (newVal, oldval) { 
         this.postBody.amountReceived = this.$store.state.objectToUpdate.amountReceived,
         this.postBody.amountPaid = this.$store.state.objectToUpdate.amountPaid,
             this.postBody.amountDue = this.$store.state.objectToUpdate.amountDue,
             this.postBody.totalContribution = this.$store.state.objectToUpdate.totalContribution,
              this.postBody.bank = this.$store.state.objectToUpdate.bank,
               this.postBody.acctno = this.$store.state.objectToUpdate.acctno,
                this.postBody.beneficiary = this.$store.state.objectToUpdate.beneficiary,
              
         this.submitorUpdate = 'Update';
               
        },
          acctno: function(newValue,oldValue){
             
                  if(newValue.length>this.textlimit)
                   this.acctno=oldValue
                }
    },
     methods: {

          assertMaxChars() {
      if (this.postBody.acctno.length > this.maxLengthInCars) {
        this.postBody.acctno = this.postBody.acctno.substring(0, this.maxLengthInCars);
      }
        },
         generateReport() {
                window.open(`/ClaimRegister/finishClaimRequest/${this.postBody.PersonID}/${this.postBody.FundTypeID}`, "_blank"); 
            },
         processRetrieve : function (discharges) {
            this.$store.state.objectToUpdate = discharges;
            this.wantshow=true;
             this.wantshow2=false;
         },
         setValuePersonID(result) {
            // alert(this.postBody.FundTypeID);
             axios.get(`/api/Claimtype/GetClaim/${result.value}/${this.postBody.FundTypeID}`)
                        .then(response => {this.claimlist=response.data})    
                                           
           },
         checkForm: function (e) {
             if (this.postBody.totalContribution) {
              e.preventDefault();
              this.canProcess = false;
              this.postPost();
          }
          else{

          this.errors = [];
            this.errors.push('Required');
          }
        },
        postPost() {
            if (this.submitorUpdate == 'Update') {
                    axios.post(`api/ClaimRegister/CreateClaim`, this.postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;
                            this.canProcess = true;
                            //  this.wantshow3=true;
                            if (response.data.responseCode == '200') {
                               // this.generateReport();
                                this.postBody.PersonID = ''; this.postBody.appdate = '';
                                this.wanttoupdate = true;

                                //this.generateReport();
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
                if (objecttoedit.totalContribution) {
                    this.postBody.amountPaid = objecttoedit.amountPaid;
                    this.postBody.amountreceived = objecttoedit.amountReceived;
                    this.postBody.amountdue = objecttoedit.amountDue;
                    this.postBody.totalContribution = objecttoedit.totalContribution;
                }
            }
        }
     }

</script>